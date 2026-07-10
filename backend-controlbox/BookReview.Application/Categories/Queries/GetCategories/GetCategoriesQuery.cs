using MediatR;
using System;
using System.Collections.Generic;

namespace BookReview.Application.Categories.Queries.GetCategories;

public record CategoryDto(Guid Id, string Name);

public record GetCategoriesQuery : IRequest<List<CategoryDto>>;