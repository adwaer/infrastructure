using System.Threading.Tasks;
using In.Di;

namespace In.Cqrs
{
    public class SimpleMsgBus : IMessageSender
    {
        private readonly IDiScope _diScope;
        private readonly IStorage<IMessage> _storage;

        public SimpleMsgBus(IDiScope diScope, IStorage<IMessage> storage)
        {
            _diScope = diScope;
            _storage = storage;
        }

        public string Send<T>(T command) where T : IMessage
        {
            SaveCommand(command);

            var handler = _diScope.Resolve<IMsgHandler<T>>();
            return handler.Handle(command);
        }

        public async Task<string> SendAsync<T>(T command) where T : IMessage
        {
            SaveCommand(command);

            var handler = _diScope.Resolve<IMsgHandler<T>>();
            return await handler.HandleAsync(command);
        }

        private void SaveCommand(IMessage command)
        {
            _storage.Add(command);
            _storage.Save(command);
        }
    }
}
