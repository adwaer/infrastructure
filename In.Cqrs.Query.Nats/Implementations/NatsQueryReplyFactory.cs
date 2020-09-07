using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using In.Common;
using In.Common.Exceptions;
using In.Cqrs.Nats.Abstract;
using In.Cqrs.Query.Nats.Adapters;
using In.Cqrs.Query.Nats.Models;
using Newtonsoft.Json;

namespace In.Cqrs.Query.Nats.Implementations
{
    public class NatsQueryReplyFactory : INatsQueryReplyFactory
    {
        private readonly ITypeFactory _typeFactory;
        private readonly INatsSerializer _serializer;

        public NatsQueryReplyFactory(ITypeFactory typeFactory,
            INatsSerializer serializer)
        {
            _typeFactory = typeFactory;
            _serializer = serializer;
        }

        public NatsQueryReplyModel Get(QueryNatsAdapter data)
        {
            var reply = data.QueryResult;
            var criterionType = _typeFactory.Get(data.CriterionType);
            var queryResultType = _typeFactory.Get(data.QueryResultType);

            return new NatsQueryReplyModel(_serializer)
            {
                Reply = reply,
                CriterionType = criterionType,
                QueryResultType = queryResultType
            };
        }
        
        public async Task<string> ExecuteQuery(object query, NatsQueryReplyModel param)
        {
            var cmd = GetCmdMethod();
            if (cmd == null)
            {
                throw new InternalException("Resolver not found");
            }

            var result = await (Task<object>) cmd.Invoke(query, new object[] {param.GetCriterion()});
            return JsonConvert.SerializeObject(result);

            MethodInfo GetCmdMethod()
            {
                var methods = query
                    .GetType()
                    .GetTypeInfo()
                    .GetDeclaredMethods("Ask");

                foreach (var method in methods)
                {
                    var contains = method.GetParameters()
                                       .FirstOrDefault()
                                       ?.ParameterType
                                       .ToString()
                                       .Contains(param.CriterionType.ToString()) == true
                                   && method.ReturnType == typeof(Task<>)
                                       .MakeGenericType(param.QueryResultType);

                    if (contains)
                    {
                        return method;
                    }
                }

                return null;
            }
        }
    }
}