using System;

namespace In.Cqrs.Nats.Abstract
{
    public interface INatsSerializer
    {
        byte[] Serialize<T>(object message);
        object Deserialize<T>(byte[] data);
        T DeserializeMsg<T>(string command, Type cmdType);
    }
}