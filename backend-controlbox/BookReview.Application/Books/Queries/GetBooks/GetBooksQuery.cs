using MediatR;

namespace BookReview.Application.Books.Queries.GetBooks;

public record BookDto(Guid Id, string Title, string Author, string CategoryName, string Summary);

public record GetBooksQuery(string? SearchTerm, Guid? CategoryId) : IRequest<List<BookDto>>;