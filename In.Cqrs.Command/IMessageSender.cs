using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace In.Cqrs.Command
{
    public interface IMessageSender
    {
        Result Send<T>(T command) where T : IMessage;
        Task<Result> SendAsync(IMessage command);
        Task<Result> SendAsync<TInput>(TInput command) where TInput : IMessage;
        Task<Result<TOutput>> SendAsync<TInput, TOutput>(TInput command) where TInput : IMessage;
    }
}
