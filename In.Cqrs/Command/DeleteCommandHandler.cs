using System;
using System.Threading.Tasks;
using In.Entity.Uow;

namespace In.Cqrs.Command
{
    public class DeleteCommandHandler<T> : IMsgHandler<DeleteCommand<T>> where T : class
    {
        private readonly IDataSetUow _dataSetUow;

        public DeleteCommandHandler(IDataSetUow dataSetUow)
        {
            _dataSetUow = dataSetUow;
        }

        public Task<string> Handle(DeleteCommand<T> message)
        {
            _dataSetUow.Remove<T>(message.Data);
            return Task.FromResult(String.Empty);
        }
    }
}
