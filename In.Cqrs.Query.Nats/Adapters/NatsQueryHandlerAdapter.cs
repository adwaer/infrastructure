using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using In.Common;
using In.Cqrs.Nats.Abstract;
using In.Cqrs.Query.Criterion.Abstract;
using In.Cqrs.Query.Queries;
using NATS.Client;

namespace In.Cqrs.Query.Nats.Adapters
{
    public class NatsQueryHandlerAdapter<TCriterion, TResult> : IQueryHandler<TCriterion, TResult>
        where TCriterion : ICriterion
    {
        private readonly INatsConnectionFactory _connectionFactory;
        private readonly INatsSerializer _serializer;
        private readonly ITypeFactory _typeFactory;
        private readonly INatsReceiverQueryQueueFactory _queueFactory;

        public NatsQueryHandlerAdapter(INatsConnectionFactory connectionFactory, INatsSerializer serializer,
            ITypeFactory typeFactory, INatsReceiverQueryQueueFactory queueFactory)
        {
            _connectionFactory = connectionFactory;
            _serializer = serializer;
            _typeFactory = typeFactory;
            _queueFactory = queueFactory;
        }

        public async Task<TResult> Ask(TCriterion criterion)
        {
            var connection = _connectionFactory.Get<QueryNatsAdapter>();

            var replySubj = GetRandomString();
            QueryNatsAdapter response;
            try
            {
                var queryQueue = _queueFactory.Get();
                var data = new QueryNatsAdapter(criterion, typeof(TResult)) {QueryResult = replySubj};

                connection.Publish(queryQueue, data);
                connection.Flush();

                response = await GetResponse(connection, replySubj, out var subscription);
                subscription.Dispose();
            }
            catch (NATSTimeoutException)
            {
                throw new Exception("Nats connection timeout exceed");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            var resultType = _typeFactory.Get(response.QueryResultType);
            if (resultType == typeof(TResult))
            {
                return _serializer.DeserializeMsg<TResult>(response.QueryResult, resultType);
            }

            throw new Exception(response.QueryResult);
        }

        private Task<QueryNatsAdapter> GetResponse(IEncodedConnection connection, string replySubj,
            out IAsyncSubscription subscription)
        {
            var promise = new TaskCompletionSource<QueryNatsAdapter>();
            var completed = 0;
            var waitTime = 0;

            ThreadPool.QueueUserWorkItem(data =>
            {
                while (completed == 0)
                {
                    if (waitTime >= 60)
                    {
                        promise.SetException(new Exception("Nats connection timeout exceed"));
                        break;
                    }

                    Thread.Sleep(1000);
                    waitTime++;
                }
            });

            subscription = connection.SubscribeAsync(replySubj,
                (sender, args) =>
                {
                    var result = (QueryNatsAdapter) args.ReceivedObject;
                    promise.SetResult(result);
                    completed++;
                });

            return promise.Task;
        }

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