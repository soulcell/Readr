using Readr.API.Models;
using System.Threading.Channels;

namespace Readr.API.Utils
{
    public class EntityQueue<T> where T : BaseEntity
    {
        private readonly Channel<T> channel;

        public EntityQueue()
        {
            channel = Channel.CreateUnbounded<T>();
        }

        public async Task EnqueueAsync(T entity) => await channel.Writer.WriteAsync(entity);

        public async Task<T> DequeueAsync(CancellationToken cancellationToken) => await channel.Reader.ReadAsync(cancellationToken);

        public IAsyncEnumerable<T> DequeueAllAsync(CancellationToken cancellationToken) => channel.Reader.ReadAllAsync(cancellationToken);
    }
}
