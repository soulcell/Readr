using Microsoft.EntityFrameworkCore.Infrastructure;
using Readr.API.Data;
using Readr.API.Models;
using Readr.API.Utils;

namespace Readr.API.Services
{
    public class BookProcessor : BackgroundService
    {
        private readonly AddCoverQueue addCoverQueue;
        private readonly IServiceScope serviceScope;
        private readonly ILogger<BookProcessor> logger;

        public BookProcessor(IServiceProvider serviceProvider, ILogger<BookProcessor> logger, AddCoverQueue addCoverQueue)
        {
            this.addCoverQueue = addCoverQueue;
            this.serviceScope = serviceProvider.CreateScope();
            this.logger = logger;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("{Name} is running.", nameof(BookProcessor));

            return ProcessBookQueueAsync(cancellationToken);
        }

        private async Task ProcessBookQueueAsync(CancellationToken cancellationToken)
        {
            await foreach (Book book in addCoverQueue.DequeueAllAsync(cancellationToken))
            {
                IBookCoverService coverService = serviceScope.ServiceProvider.GetRequiredService<IBookCoverService>();
                ReadrDbContext dbContext = serviceScope.ServiceProvider.GetRequiredService<ReadrDbContext>();

                dbContext.Attach(book);

                string? coverUrl = await coverService.FindBookCoverUrlAsync(book.BookTitle);

                if (string.IsNullOrWhiteSpace(coverUrl))
                {
                    logger.LogError("Couldn't find cover for book {book}", book.BookTitle.Title);
                    continue;
                }

                book.BookTitle!.Cover = new BookCover(0, coverUrl);

                dbContext.BookTitles.Update(book.BookTitle);
                await dbContext.SaveChangesAsync();
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            serviceScope.Dispose();
        }
    }
}
