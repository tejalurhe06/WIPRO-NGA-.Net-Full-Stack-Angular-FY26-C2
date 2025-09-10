using BookStoreApi.Models;

namespace BookStoreApi.Data;

public static class SeedData
{
    public static void Initialize(BookStoreContext context)
    {
        if (!context.Authors.Any())
        {
            var authors = new Author[]
            {
                new Author { Name = "J.K. Rowling" },
                new Author { Name = "George R.R. Martin" },
                new Author { Name = "Stephen King" }
            };

            context.Authors.AddRange(authors);
            context.SaveChanges();

            var books = new Book[]
            {
                new Book { 
                    Title = "Harry Potter and the Philosopher's Stone", 
                    PublicationYear = 1997, 
                    AuthorId = authors[0].Id 
                },
                new Book { 
                    Title = "Harry Potter and the Chamber of Secrets", 
                    PublicationYear = 1998, 
                    AuthorId = authors[0].Id 
                },
                new Book { 
                    Title = "A Game of Thrones", 
                    PublicationYear = 1996, 
                    AuthorId = authors[1].Id 
                },
                new Book { 
                    Title = "A Clash of Kings", 
                    PublicationYear = 1998, 
                    AuthorId = authors[1].Id 
                },
                new Book { 
                    Title = "The Shining", 
                    PublicationYear = 1977, 
                    AuthorId = authors[2].Id 
                }
            };

            context.Books.AddRange(books);
            context.SaveChanges();
        }
    }
}