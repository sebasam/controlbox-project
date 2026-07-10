using BookReview.Domain.Common;

namespace BookReview.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; private set; }

    private Category() { }

    public Category(string name)
    {
        Name = name;
    }
}