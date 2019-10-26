using System.Runtime.Serialization;

namespace In.Cqrs.Command.Nats.Adapters
{
    [DataContract]
    public class ResultAdapter
    {
        [DataMember]
        public string Data { get; set; }
        [DataMember]
        public bool IsSuccess { get; set; }
    }
}