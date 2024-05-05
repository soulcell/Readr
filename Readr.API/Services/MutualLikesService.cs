using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Readr.API.Data;
using Readr.API.DTOs;
using Readr.API.Hubs;
using Readr.API.Models;

namespace Readr.API.Services
{
    public class MutualLikesService : BackgroundService
    {
        private int executionCount = 0;
        private readonly IServiceProvider serviceProvider;
        private readonly IHubContext<NotificationHub, INotificationClient> hubContext;
        private readonly ILogger<MutualLikesService> logger;

        public MutualLikesService(IServiceProvider serviceProvider, IHubContext<NotificationHub, INotificationClient> hubContext, ILogger<MutualLikesService> logger)
        {
            this.serviceProvider = serviceProvider;
            this.hubContext = hubContext;
            this.logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            logger.LogInformation("Timed Hosted Service running.");
            using var timer = new PeriodicTimer(TimeSpan.FromSeconds(5));

            while (await timer.WaitForNextTickAsync(stoppingToken))
            {
                await DoWork();
            }
        }

        private async Task DoWork()
        {
            var count = Interlocked.Increment(ref executionCount);
            using IServiceScope scope = this.serviceProvider.CreateScope();
            using ReadrDbContext dbContext = scope.ServiceProvider.GetRequiredService<ReadrDbContext>();


            foreach (BookLike like in dbContext.BookLikes
                .Where(bl => bl.Status == BookLikeStatus.Like)
                .Include(bl => bl.User)
                .Include(bl => bl.Book)
                .ThenInclude(b => b.Owner)
                .ToList()) // TODO: optimize this
            {
                foreach (BookLike mutualLike in dbContext.BookLikes
                    .Where(bl => bl.Status == BookLikeStatus.Like)
                    .Where(bl => bl.User == like.Book.Owner)
                    .Where(bl => bl.Book.Owner == like.User)
                    .ToList())
                {
                    if (mutualLike.Status != BookLikeStatus.Like || like.Status != BookLikeStatus.Like) continue; // fixes double notification

                    await dbContext.Entry(like).Reference(bl => bl.Book).Query().Include(b => b.BookTitle).LoadAsync();
                    await dbContext.Entry(mutualLike).Reference(bl => bl.Book).Query().Include(b => b.BookTitle).LoadAsync();

                    await NotifyUser(like.User, mutualLike.User, mutualLike.Book, like.Book);
                    await NotifyUser(mutualLike.User, like.User, like.Book, mutualLike.Book);

                    like.Status = BookLikeStatus.Notified;
                    mutualLike.Status = BookLikeStatus.Notified;
                }
            }
            await dbContext.SaveChangesAsync();

            

            logger.LogInformation(
                "Timed Hosted Service is working. Count: {Count}", count);
        }

        private async Task NotifyUser(User receiver, User likedUser, Book receiverBook, Book hisBook)
        {
            await this.hubContext.Clients.User(receiver.Id.ToString()).ReceiveMutualLikeNotification(likedUser.AsDto(), hisBook.AsDto(), receiverBook.AsDto());
            this.logger.LogInformation("Notified user {user}", receiver);
        }

        
    }
}
