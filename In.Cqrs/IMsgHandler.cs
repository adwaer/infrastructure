using System.Threading.Tasks;

namespace In.Cqrs
{
    public interface IMsgHandler<in T> where T: IMessage
    {
        string Handle(T message);
        Task<string> HandleAsync(T message);
    }
}
