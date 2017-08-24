using In.Domain;

namespace In.Cqrs.Command
{
    public class DeleteCommand<T> : IMessage
    {
        public T Data { get; }

        public DeleteCommand(T data)
        {
            Data = data;
        }
    }
}
