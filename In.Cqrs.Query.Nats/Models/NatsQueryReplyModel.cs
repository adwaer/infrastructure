using System;
using In.Cqrs.Nats.Abstract;
using In.Cqrs.Query.Criterion.Abstract;
using In.Cqrs.Query.Queries;

namespace In.Cqrs.Query.Nats.Models
{
    public class NatsQueryReplyModel
    {
        private readonly INatsSerializer _serializer;

        public NatsQueryReplyModel(INatsSerializer serializer)
        {
            _serializer = serializer;
        }

        public string Reply { get; set; }
        public string Criterion { get; set; }
        public Type CriterionType { get; set; }
        public Type QueryResultType { get; set; }

        public Type GetQueryType()
        {
            return typeof(IQueryHandler<,>)
                .MakeGenericType(CriterionType, QueryResultType);
        }

        public ICriterion GetCriterion()
        {
            try
            {
                return _serializer.DeserializeMsg<ICriterion>(Criterion, CriterionType);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Error when deserializing {ToString()}",
                    ex);
            }
        }

        public override string ToString()
        {
            return $"{CriterionType} {QueryResultType}";
        }
    }
}