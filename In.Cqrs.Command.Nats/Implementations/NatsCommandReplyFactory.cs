using System.Threading.Tasks;
using In.Common;
using In.Cqrs.Command.Nats.Adapters;
using In.Cqrs.Command.Nats.Models;
using In.Cqrs.Nats.Abstract;
using In.FunctionalCSharp;

namespace In.Cqrs.Command.Nats.Implementations
{
    public class NatsCommandReplyFactory : INatsCommandReplyFactory
    {
        private readonly ITypeFactory _typeFactory;
        private readonly INatsSerializer _serializer;
        private readonly IDiScope _diScope;

        public NatsCommandReplyFactory(ITypeFactory typeFactory, INatsSerializer serializer, IDiScope diScope)
        {
            _typeFactory = typeFactory;
            _serializer = serializer;
            _diScope = diScope;
        }

        public NatsCommandReplyModel Get(CommandNatsAdapter data)
        {
            var reply = data.Reply;
            var cmdType = _typeFactory.Get(data.CommandType);
            if (cmdType == null)
            {
                return null;
            }

            return new NatsCommandReplyModel(_serializer)
            {
                CmdType = cmdType,
                Reply = reply
            };
        }

        public Task<Result> ExecuteCmd(IMessage message)
        {
            return _diScope.Resolve<IMessageSender>()
                .SendAsync(message);
        }
    }
}