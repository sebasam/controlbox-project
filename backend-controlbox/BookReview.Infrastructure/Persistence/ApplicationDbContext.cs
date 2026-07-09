using BookReview.Domain.Entities;
using BookReview.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookReview.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Review> Reviews => Set<Review>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Book>(b =>
        {
            b.HasKey(x => x.Id);
            b.Property(x => x.Title).IsRequired().HasMaxLength(200);
            b.Property(x => x.Author).IsRequired().HasMaxLength(150);
            b.HasOne(x => x.Category).WithMany().HasForeignKey(x => x.CategoryId);
            b.HasMany(x => x.Reviews).WithOne().HasForeignKey(x => x.BookId);
        });

        builder.Entity<Category>(c =>
        {
            c.HasKey(x => x.Id);
            c.Property(x => x.Name).IsRequired().HasMaxLength(100);
        });

        builder.Entity<Review>(r =>
        {
            r.HasKey(x => x.Id);
            r.Property(x => x.Comment).IsRequired().HasMaxLength(1000);
            r.HasOne<ApplicationUser>().WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        });
    }
}