using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using In.Common;
using In.Cqrs.Command.Nats.Adapters;
using In.Cqrs.Nats.Abstract;
using In.DataAccess.Repository.Abstract;
using NATS.Client;
using Newtonsoft.Json.Linq;

namespace In.Cqrs.Command.Nats.Implementations
{
    public class NatsMessageBus : IMessageSender, IDisposable
    {
        private readonly IDiScope _diScope;

        private readonly IRepository<IMessageResult> _storage;
        private readonly INatsReceiverCommandQueueFactory _queueFactory;
        private readonly IEncodedConnection _connection;
        private readonly IEncodedConnection _responseConnection;
        private readonly List<IAsyncSubscription> _subscriptions = new List<IAsyncSubscription>();

        public NatsMessageBus(IDiScope diScope, INatsConnectionFactory connectionFactory,
            IRepository<IMessageResult> storage, INatsReceiverCommandQueueFactory queueFactory)
        {
            _diScope = diScope;
            _storage = storage;
            _queueFactory = queueFactory;
            _connection = connectionFactory.Get<CommandNatsAdapter>();
            _responseConnection = connectionFactory.Get<ResultAdapter>();
        }

        public Result Send<T>(T command) where T : IMessage
        {
            throw new NotImplementedException();
        }

        public Task<Result> SendAsync(IMessage command)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> SendAsync<TInput>(TInput command) where TInput : IMessage
        {
            var messageResult = GetLogModel(command);
            try
            {
                var commandQueue = _queueFactory.Get();

                var data = new CommandNatsAdapter(command);
                _connection.Publish(commandQueue.Value, data.Reply, data);
                _connection.Flush();

                return await GetResponse(data.Reply);
            }
            catch (Exception e)
            {
                messageResult.Socceed = false;
                messageResult.Info = e.ToString();
                throw;
            }
            finally
            {
                await SaveCommand(messageResult);
            }
        }

        public Task<Result<TOutput>> SendAsync<TInput, TOutput>(TInput command) where TInput : IMessage
        {
            throw new NotSupportedException();
        }

        public void Dispose()
        {
            foreach (var subscription in _subscriptions)
            {
                subscription.Dispose();
            }
        }
        
        #region private

        private Task<Result> GetResponse(string dataReply)
        {
            var promise = new TaskCompletionSource<Result>();
            var completed = 0;
            var waitTime = 0;

            ThreadPool.QueueUserWorkItem(data =>
            {
                while (completed == 0)
                {
                    if (waitTime >= 60)
                    {
                        promise.SetResult(Result.Failure("Nats connection timeout exceed"));
                        break;
                    }
                    
                    Thread.Sleep(1000);
                    waitTime++;
                }
            });

            var subscription = _responseConnection.SubscribeAsync(dataReply, "responseQueue",
                (sender, args) =>
                {
                    var result = (ResultAdapter) args.ReceivedObject;
                    promise.SetResult(result.IsSuccess ? Result.Success() : Result.Failure(result.Data));
                    completed++;
                });
            _subscriptions.Add(subscription);
            
            return promise.Task;
        }

        private Task SaveCommand(IMessageResult msgResult)
        {
            _storage.Add(msgResult);
            return _storage.Save();
        }

        private IMessageResult GetLogModel(IMessage command)
        {
            var msgResult = _diScope.Resolve<IMessageResult>();
            msgResult.Body = JObject.FromObject(command).ToString();
            msgResult.Type = command.GetType().ToString();
            msgResult.Socceed = true;

            return msgResult;
        }

        
        #endregion
    }
}