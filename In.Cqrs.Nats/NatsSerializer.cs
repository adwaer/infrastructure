using System;
using System.IO;
using System.Runtime.Serialization.Json;
using In.Cqrs.Nats.Abstract;
using Newtonsoft.Json;

namespace In.Cqrs.Nats
{
    public class NatsSerializer : INatsSerializer
    {
        public byte[] Serialize<T>(object message)
        {
            if (message == null)
                return null;

            var serializer = new DataContractJsonSerializer(typeof(T));
            using MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, message);
            return stream.ToArray();
        }

        private static readonly Type[] PrimitiveTypes =
        {
            typeof(string),
            typeof(char),
            typeof(byte),
            typeof(sbyte),
            typeof(ushort),
            typeof(short),
            typeof(uint),
            typeof(int),
            typeof(ulong),
            typeof(long),
            typeof(float),
            typeof(double),
            typeof(decimal),
            typeof(DateTime)
        };

        public object Deserialize<T>(byte[] data)
        {
            using var stream = new MemoryStream();
            var serializer = new DataContractJsonSerializer(typeof(T));
            stream.Write(data, 0, data.Length);
            stream.Position = 0;
            return serializer.ReadObject(stream);
        }

        public T DeserializeMsg<T>(string command, Type cmdType = null)
        {
            cmdType ??= typeof(T);
            return (T) (IsPrimitive(cmdType) ? command : JsonConvert.DeserializeObject(command, cmdType));
        }

        private static bool IsPrimitive(Type type)
        {
            return Array.IndexOf(PrimitiveTypes, type) >= 0;
        }
    }
}