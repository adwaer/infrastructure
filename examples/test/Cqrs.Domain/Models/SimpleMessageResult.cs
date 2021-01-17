using In.Cqrs.Command;
using In.DataAccess.Entity;

namespace Cqrs.Domain.Models
{
    public class SimpleMessageResult : HasKeyBase<long>, IMessageResult
    {
        public string Body { get; set; }
        public string Info { get; set; }
        public bool Succeed { get; set; }
        public string Type { get; set; }
    }
}