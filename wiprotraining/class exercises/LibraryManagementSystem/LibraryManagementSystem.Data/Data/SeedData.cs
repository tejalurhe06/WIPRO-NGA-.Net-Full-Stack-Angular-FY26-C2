using LibraryManagementSystem.Core.Models;

namespace LibraryManagementSystem.Data.Data
{
    public static class SeedData
    {
        public static void Initialize(LibraryDbContext context)
        {
            if (!context.Authors.Any())
            {
                var authors = new List<Author>
                {
                    new Author { Name = "J.K. Rowling", Biography = "British author best known for the Harry Potter series.", DateOfBirth = new DateTime(1965, 7, 31) },
                    new Author { Name = "George R.R. Martin", Biography = "American novelist and short-story writer.", DateOfBirth = new DateTime(1948, 9, 20) },
                    new Author { Name = "J.R.R. Tolkien", Biography = "English writer, poet, philologist, and academic.", DateOfBirth = new DateTime(1892, 1, 3) }
                };
                context.Authors.AddRange(authors);
                context.SaveChanges();
            }

            if (!context.Genres.Any())
            {
                var genres = new List<Genre>
                {
                    new Genre { Name = "Fantasy", Description = "Fantasy literature" },
                    new Genre { Name = "Science Fiction", Description = "Sci-Fi literature" },
                    new Genre { Name = "Mystery", Description = "Mystery literature" }
                };
                context.Genres.AddRange(genres);
                context.SaveChanges();
            }

            if (!context.Books.Any())
            {
                var books = new List<Book>
                {
                    new Book { Title = "Harry Potter and the Philosopher's Stone", ISBN = "9780747532743", Description = "First book in the Harry Potter series", PublishedDate = new DateTime(1997, 6, 26), AuthorId = 1, GenreId = 1 },
                    new Book { Title = "A Game of Thrones", ISBN = "9780553103540", Description = "First book in A Song of Ice and Fire series", PublishedDate = new DateTime(1996, 8, 1), AuthorId = 2, GenreId = 1 },
                    new Book { Title = "The Hobbit", ISBN = "9780547928227", Description = "Fantasy novel set in Middle-earth", PublishedDate = new DateTime(1937, 9, 21), AuthorId = 3, GenreId = 1 }
                };
                context.Books.AddRange(books);
                context.SaveChanges();
            }
        }
    }
}