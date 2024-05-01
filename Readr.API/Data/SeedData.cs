using Readr.API.Models;

namespace Readr.API.Data
{
    public static class SeedData
    {
        public static async Task SeedAsync(ReadrDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            List<Genre> genres = new()
            {
                new Genre(0, "Fiction"),
                new Genre(0, "Thriller" ),
                new Genre(0, "Science Fiction"),
                new Genre(0, "Fantasy"),
                new Genre(0, "Historical Fiction"),
                new Genre(0, "Adventure"),
                new Genre(0, "Dystopian"),
                new Genre(0, "Utopian"),
                new Genre(0, "Crime"),
                new Genre(0, "Horror"),
                new Genre(0, "Memoir"),
                new Genre(0, "Biography"),
                new Genre(0, "Autobiography"),
                new Genre(0, "Satire"),
                new Genre(0, "Romance"),
                new Genre(0, "Humor"),
                new Genre(0, "Political Fiction"),
                new Genre(0, "Western"),
                new Genre(0, "Historical Romance"),
                new Genre(0, "Young Adult"),
                new Genre(0, "Children\'s Literature"),
                new Genre(0, "Fairy Tale"),
                new Genre(0, "Poetry"),
            };

            List<BookTitle> bookTitles = new()
            {
                new BookTitle(0, "To Kill a Mockingbird", "Harper Lee", genres[0]),
                new BookTitle(0, "1984", "George Orwell", genres[6]),
                new BookTitle(0, "The Great Gatsby", "F. Scott Fitzgerald", genres[0]),
                new BookTitle(0, "Pride and Prejudice", "Jane Austen", genres[14]),
                new BookTitle(0, "The Catcher in the Rye", "J.D. Salinger", genres[0]),
                new BookTitle(0, "The Lord of the Rings", "J.R.R. Tolkien", genres[3]),
                new BookTitle(0, "Harry Potter and the Philosopher's Stone", "J.K. Rowling", genres[3]),
                new BookTitle(0, "Brave New World", "Aldous Huxley", genres[6]),
                new BookTitle(0, "The Hobbit", "J.R.R. Tolkien", genres[3]),
                new BookTitle(0, "The Da Vinci Code", "Dan Brown", genres[1]),
            };

            List<User> users = new()
            {
                new User("00000000"),
                new User("11111111"),
                new User("22222222")
            };

            List<Book> books = new()
            {
                new Book(0, bookTitles[0], users[0], 0, 0),
                new Book(0, bookTitles[1], users[1], 0, 0),
                new Book(0, bookTitles[2], users[0], 0, 0),
                new Book(0, bookTitles[3], users[1], 0, 0),
            };

            await context.AddRangeAsync(genres);
            await context.AddRangeAsync(bookTitles);
            await context.AddRangeAsync(users);
            await context.AddRangeAsync(books);
            await context.SaveChangesAsync();
        }
    }
}
