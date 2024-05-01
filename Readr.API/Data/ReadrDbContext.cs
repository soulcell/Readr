using Microsoft.EntityFrameworkCore;
using Readr.API.Models;

namespace Readr.API.Data
{
    public class ReadrDbContext : DbContext
    {
        public ReadrDbContext(DbContextOptions<ReadrDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<BookTitle> BookTitles { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<BookLike> BookLikes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<BookTitle>()
                .HasOne(bt => bt.Genre)
                .WithMany();

            modelBuilder.Entity<BookTitle>()
                .HasOne(bt => bt.Cover)
                .WithMany();

            modelBuilder.Entity<Book>()
                .HasOne(b => b.BookTitle)
                .WithMany();

            modelBuilder.Entity<User>()
                .HasMany(u => u.Books)
                .WithOne(b => b.Owner);

            modelBuilder.Entity<User>()
                .HasMany(u => u.PreferedGeneres)
                .WithMany();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.PhoneNumber)
                .IsUnique(true);

            modelBuilder.Entity<BookLike>()
                .HasOne(bl => bl.User)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<BookLike>()
                .HasOne(bl => bl.Book)
                .WithMany();
        }
    }
}
