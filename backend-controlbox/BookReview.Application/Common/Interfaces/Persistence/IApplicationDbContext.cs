using BookReview.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookReview.Application.Common.Interfaces.Persistence;

public interface IApplicationDbContext
{
    DbSet<Book> Books { get; }
    DbSet<Category> Categories { get; }
    DbSet<Review> Reviews { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}