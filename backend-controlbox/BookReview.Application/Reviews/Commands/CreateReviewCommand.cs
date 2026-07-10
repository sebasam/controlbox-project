using MediatR;

namespace BookReview.Application.Reviews.Commands;

public record CreateReviewCommand(Guid BookId, int Rating, string Comment, string UserId) : IRequest<Guid>;