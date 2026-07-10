using BookReview.Application.Common.Interfaces.Persistence;
using BookReview.Domain.Entities;
using MediatR;

namespace BookReview.Application.Reviews.Commands;

public class CreateReviewCommandHandler : IRequestHandler<CreateReviewCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateReviewCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = new Review(request.Rating, request.Comment, request.BookId, request.UserId);
        
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync(cancellationToken);

        return review.Id;
    }
}