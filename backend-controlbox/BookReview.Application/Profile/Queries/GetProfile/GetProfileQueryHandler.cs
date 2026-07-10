using BookReview.Application.Common.Interfaces.Authentication;
using BookReview.Application.Common.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookReview.Application.Profile.Queries.GetProfile;

public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, ProfileDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;

    public GetProfileQueryHandler(IApplicationDbContext context, IIdentityService identityService)
    {
        _context = context;
        _identityService = identityService;
    }

    public async Task<ProfileDto> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var userName = await _identityService.GetUserNameAsync(request.UserId);
        var pictureUrl = await _identityService.GetProfilePictureAsync(request.UserId);

        var reviews = await _context.Reviews
            .Where(r => r.UserId == request.UserId)
            .Join(_context.Books, 
                  r => r.BookId, 
                  b => b.Id, 
                  (r, b) => new { Review = r, Book = b })
            .OrderByDescending(x => x.Review.CreatedAt)
            .Select(x => new UserReviewDto(
                x.Review.Id, 
                x.Book.Id, 
                x.Book.Title, 
                x.Review.Rating, 
                x.Review.Comment, 
                x.Review.CreatedAt))
            .ToListAsync(cancellationToken);

        return new ProfileDto(userName, request.Email, pictureUrl, reviews);
    }
}