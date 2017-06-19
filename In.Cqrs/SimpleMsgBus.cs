using System;
using System.Threading.Tasks;
using In.Di;
using Newtonsoft.Json.Linq;

namespace In.Cqrs
{
    public class SimpleMsgBus : IMessageSender
    {
        private readonly IDiScope _diScope;
        private readonly IStorage<IMessageResult> _storage;

        public SimpleMsgBus(IDiScope diScope, IStorage<IMessageResult> storage)
        {
            _diScope = diScope;
            _storage = storage;
        }

        public string Send<T>(T command) where T : IMessage
        {
            return AsyncHelpers.RunSync(() => SendAsync(command));
        }

        public async Task<string> SendAsync<T>(T command) where T : IMessage
        {
            var messageResult = GetLogModel(command);

            try
            {
                var handler = _diScope.Resolve<IMsgHandler<T>>();
                return await handler.Handle(command);
            }
            catch (Exception e)
            {
                messageResult.Socceed = false;
                messageResult.Info = e.ToString();
                throw;
            }
            finally
            {
                SaveCommand(messageResult);
            }
        }

        private void SaveCommand(IMessageResult msgResult)
        {
            _storage.Add(msgResult);
            _storage.Save(msgResult);
        }

        private IMessageResult GetLogModel(IMessage command)
        {
            var msgResult = _diScope.Resolve<IMessageResult>();
            msgResult.Body = JObject.FromObject(command).ToString();
            msgResult.Type = command.GetType().ToString();
            msgResult.Socceed = true;

            return msgResult;
        }
    }
}
