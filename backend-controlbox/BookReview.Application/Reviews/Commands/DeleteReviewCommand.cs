using MediatR;
using System;

namespace BookReview.Application.Reviews.Commands;

public record DeleteReviewCommand(Guid Id, string UserId) : IRequest<bool>;