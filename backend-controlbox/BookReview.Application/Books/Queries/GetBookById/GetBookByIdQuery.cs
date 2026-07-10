using MediatR;
using System;
using System.Collections.Generic;

namespace BookReview.Application.Books.Queries.GetBookById;

public record ReviewDto(Guid Id, int Rating, string Comment, string UserName, string UserId, DateTime CreatedAt);
public record BookDetailDto(Guid Id, string Title, string Author, string CategoryName, string Summary, List<ReviewDto> Reviews);

public record GetBookByIdQuery(Guid Id) : IRequest<BookDetailDto>;