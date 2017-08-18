using System;
using System.Threading.Tasks;
using In.Entity.Uow;

namespace In.Cqrs.Command
{
    public class DeleteCommandHandler : IMsgHandler<DeleteCommand>
    {
        private readonly IDataSetUow _dataSetUow;

        public DeleteCommandHandler(IDataSetUow dataSetUow)
        {
            _dataSetUow = dataSetUow;
        }

        public Task<string> Handle(DeleteCommand message)
        {
            _dataSetUow.Remove(message.Data);
            return Task.FromResult(String.Empty);
        }
    }
}
