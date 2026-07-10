using BookReview.Application.Common.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookReview.Application.Books.Queries.GetBooks;

public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<BookDto>>
{
    private readonly IApplicationDbContext _context;

    public GetBooksQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Books.Include(b => b.Category).AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = query.Where(b => b.Title.ToLower().Contains(request.SearchTerm.ToLower()) || 
                                     b.Author.ToLower().Contains(request.SearchTerm.ToLower()));
        }

        if (request.CategoryId.HasValue)
        {
            query = query.Where(b => b.CategoryId == request.CategoryId.Value);
        }

        return await query
            .Select(b => new BookDto(b.Id, b.Title, b.Author, b.Category.Name, b.Summary))
            .ToListAsync(cancellationToken);
    }
}