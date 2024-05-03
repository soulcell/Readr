using Readr.API.Models;
using System.Threading.Channels;

namespace Readr.API.Utils
{
    public class AddCoverQueue
    {

        private readonly Channel<Book> channel;

        public AddCoverQueue()
        {
            channel = Channel.CreateUnbounded<Book>();
        }

        public async Task EnqueueAsync(Book book) => await channel.Writer.WriteAsync(book);

        public async Task<Book> DequeueAsync(CancellationToken cancellationToken) => await channel.Reader.ReadAsync(cancellationToken);

        public IAsyncEnumerable<Book> DequeueAllAsync(CancellationToken cancellationToken) => channel.Reader.ReadAllAsync(cancellationToken);
    }
}
