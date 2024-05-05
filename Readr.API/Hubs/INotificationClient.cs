using Readr.API.DTOs;

namespace Readr.API.Hubs
{
    public interface INotificationClient
    {
        Task ReceiveMutualLikeNotification(UserDto user, BookDto hisBook, BookDto myBook);
    }
}
