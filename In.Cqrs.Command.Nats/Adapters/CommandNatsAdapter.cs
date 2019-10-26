using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace In.Cqrs.Command.Nats.Adapters
{
    [DataContract]
    public class CommandNatsAdapter
    {
        [DataMember]
        public string Command { get; set; }
        [DataMember]
        public string CommandType { get; set; }

        public CommandNatsAdapter(object command)
        {
            Command = JsonConvert.SerializeObject(command);
            CommandType = command.GetType().ToString();
        }
        
        public CommandNatsAdapter(){}
    }
}