using System.Threading.Tasks;

namespace In.Cqrs
{
    public interface IMsgHandler<in T> where T: IMessage
    {
        Task<string> Handle(T message);
    }
}
