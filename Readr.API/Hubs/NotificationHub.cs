using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Readr.API.Data;

namespace Readr.API.Hubs
{
    [Authorize]
    public class NotificationHub : Hub<INotificationClient>
    {
        private readonly ReadrDbContext dbContext;
        private readonly ILogger<NotificationHub> logger;

        public NotificationHub(ReadrDbContext dbContext, ILogger<NotificationHub> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public override Task OnConnectedAsync()
        {
            this.logger.LogInformation("User {user} connected to the notification hub", Context.UserIdentifier);

            return base.OnConnectedAsync();
        }
    }
}
