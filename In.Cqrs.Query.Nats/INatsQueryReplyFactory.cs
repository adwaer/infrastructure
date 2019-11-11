using System.Threading.Tasks;
using In.Cqrs.Query.Nats.Adapters;
using In.Cqrs.Query.Nats.Models;

namespace In.Cqrs.Query.Nats
{
    public interface INatsQueryReplyFactory
    {
        NatsQueryReplyModel Get(QueryNatsAdapter data);
        Task<string> ExecuteQuery(object query, NatsQueryReplyModel param);
    }
}