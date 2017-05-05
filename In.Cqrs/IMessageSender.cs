using System.Threading.Tasks;

namespace In.Cqrs
{
    public interface IMessageSender
    {
        string Send<T>(T command) where T : IMessage;
        Task<string> SendAsync<T>(T command) where T : IMessage;
    }
}
