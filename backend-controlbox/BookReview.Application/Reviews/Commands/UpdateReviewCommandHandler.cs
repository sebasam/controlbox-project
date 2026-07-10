using BookReview.Application.Common.Interfaces.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace BookReview.Application.Reviews.Commands;

public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public UpdateReviewCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var review = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == request.Id, cancellationToken);

        if (review == null) throw new Exception("Review not found");
        if (review.UserId != request.UserId) throw new UnauthorizedAccessException("You are not the owner of this review");

        review.Update(request.Rating, request.Comment);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}