using BookReview.Domain.Entities;

namespace BookReview.Infrastructure.Persistence;

public static class ApplicationDbContextSeed
{
    public static async Task SeedSampleDataAsync(ApplicationDbContext context)
    {
        if (!context.Categories.Any())
        {
            var category1 = new Category("Ciencia Ficción");
            var category2 = new Category("Fantasía");
            var category3 = new Category("Desarrollo Personal");
            
            context.Categories.AddRange(category1, category2, category3);
            
            var book1 = new Book(
                "Dune", 
                "Frank Herbert", 
                "Arrakis, el planeta del desierto, es la única fuente de melange, la especia más valiosa del universo.", 
                category1.Id);
                
            var book2 = new Book(
                "Hábitos Atómicos", 
                "James Clear", 
                "Un método sencillo y comprobado para desarrollar buenos hábitos y eliminar los malos.", 
                category3.Id);

            context.Books.AddRange(book1, book2);
            await context.SaveChangesAsync();
        }
    }
}