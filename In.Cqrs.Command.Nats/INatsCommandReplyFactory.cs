using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using In.Cqrs.Command.Nats.Adapters;
using In.Cqrs.Command.Nats.Models;

namespace In.Cqrs.Command.Nats
{
    public interface INatsCommandReplyFactory
    {
        NatsCommandReplyModel Get(CommandNatsAdapter data);
        Task<Result> ExecuteCmd(IMessage message);
    }
}