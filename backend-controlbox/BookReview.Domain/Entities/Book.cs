using BookReview.Domain.Common;

namespace BookReview.Domain.Entities;

public class Book : BaseEntity
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    public string Summary { get; private set; }
    public Guid CategoryId { get; private set; }
    public Category Category { get; private set; }
    
    private readonly List<Review> _reviews = new();
    public IReadOnlyCollection<Review> Reviews => _reviews.AsReadOnly();

    private Book() { }

    public Book(string title, string author, string summary, Guid categoryId)
    {
        Title = title;
        Author = author;
        Summary = summary;
        CategoryId = categoryId;
    }
}