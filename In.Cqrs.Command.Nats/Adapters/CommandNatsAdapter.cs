using System;
using System.Runtime.Serialization;
using System.Text;
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
        [DataMember]
        public string Reply { get; set; }

        public CommandNatsAdapter(object command)
        {
            Command = JsonConvert.SerializeObject(command);
            CommandType = command.GetType().ToString();
            Reply = GetRandomString();
        }
        
        public CommandNatsAdapter(){}
        
        private string GetRandomString(bool lowerCase = true, int size = 7)
        {
            var builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (var i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
    }
}