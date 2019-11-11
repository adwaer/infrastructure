using System;
using In.Cqrs.Nats.Abstract;

namespace In.Cqrs.Command.Nats.Models
{
    public class NatsCommandReplyModel
    {
        private readonly INatsSerializer _serializer;
        public Type CmdType { get; set; }
        public string Command { get; set; }
        public string Reply { get; set; }

        public NatsCommandReplyModel(INatsSerializer serializer)
        {
            _serializer = serializer;
        }


        public override string ToString()
        {
            return CmdType.ToString();
        }

        public IMessage GetCommand()
        {
            try
            {
                return _serializer.DeserializeMsg<IMessage>(Command, CmdType);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Error when deserializing {ToString()}",
                    ex);
            }
        }
    }
}