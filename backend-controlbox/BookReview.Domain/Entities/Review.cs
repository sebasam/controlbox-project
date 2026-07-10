using BookReview.Domain.Common;

namespace BookReview.Domain.Entities;

public class Review : BaseEntity
{
    public int Rating { get; private set; }
    public string Comment { get; private set; }
    public Guid BookId { get; private set; }
    public string UserId { get; private set; }

    private Review() { }

    public Review(int rating, string comment, Guid bookId, string userId)
    {
        if (rating < 1 || rating > 5)
            throw new ArgumentOutOfRangeException(nameof(rating));

        Rating = rating;
        Comment = comment;
        BookId = bookId;
        UserId = userId;
    }

    public void Update(int rating, string comment)
    {
        if (rating < 1 || rating > 5)
            throw new ArgumentOutOfRangeException(nameof(rating));

        Rating = rating;
        Comment = comment;
        UpdatedAt = DateTime.UtcNow;
    }
}