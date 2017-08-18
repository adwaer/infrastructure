using In.Domain;

namespace In.Cqrs.Command
{
    public class DeleteCommand : IMessage
    {
        public IEntity Data { get; }

        public DeleteCommand(IEntity data)
        {
            Data = data;
        }
    }
}
