using MediatR;
using System;

namespace BookReview.Application.Reviews.Commands;

public record UpdateReviewCommand(Guid Id, int Rating, string Comment, string UserId) : IRequest<bool>;