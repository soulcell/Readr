﻿namespace Readr.API.Models
{
    public class BookLike : BaseEntity
    {
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }

        public Book Book { get; set; }

        public BookLikeStatus Status { get; set; }

        public BookLike(int id, User user, Book book, BookLikeStatus status ) : base(id)
        {
            User = user;
            Book = book;
            Status = status;
        }
    }

    public enum BookLikeStatus
    {
        Dislike = 0,
        Like = 1,
    }
}