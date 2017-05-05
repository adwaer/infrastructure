using System.Threading;
using System.Threading.Tasks;

namespace In.Cqrs.Command
{
    public abstract class BaseCommandHandler<T> : IMsgHandler<T> where T : IMessage
    {
        public virtual string Handle(T message)
        {
            return HandleAsync(message)
                .ConfigureAwait(false)
                .GetAwaiter()
                .GetResult();
        }

        public virtual Task<string> HandleAsync(T message)
        {
            return Task.Factory.StartNew(() => Handle(message),
                CancellationToken.None,
                TaskCreationOptions.None,
                TaskScheduler.FromCurrentSynchronizationContext());
        }
    }
}