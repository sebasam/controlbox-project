using BookReview.Application.Common.Interfaces.Authentication;
using BookReview.Application.Common.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookReview.Application.Books.Queries.GetBookById;

public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDetailDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;

    public GetBookByIdQueryHandler(IApplicationDbContext context, IIdentityService identityService)
    {
        _context = context;
        _identityService = identityService;
    }

    public async Task<BookDetailDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
    {
        var book = await _context.Books
            .Include(b => b.Category)
            .FirstOrDefaultAsync(b => b.Id == request.Id, cancellationToken);

        if (book == null) throw new Exception("Book not found");

        var reviews = await _context.Reviews
            .Where(r => r.BookId == request.Id)
            .OrderByDescending(r => r.CreatedAt)
            .ToListAsync(cancellationToken);

        var reviewDtos = new List<ReviewDto>();
        foreach (var r in reviews)
        {
            var userName = await _identityService.GetUserNameAsync(r.UserId);
            reviewDtos.Add(new ReviewDto(r.Id, r.Rating, r.Comment, userName, r.CreatedAt));
        }

        return new BookDetailDto(book.Id, book.Title, book.Author, book.Category.Name, book.Summary, reviewDtos);
    }
}