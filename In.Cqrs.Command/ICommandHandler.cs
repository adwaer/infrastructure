using System.Threading.Tasks;
using CSharpFunctionalExtensions;

namespace In.Cqrs.Command
{
    public interface ICommandHandler<in T> where T: IMessage
    {
        Task<Result> Handle(T message);
    }

    public interface ICommandHandler<in TInput, TOutput> where TInput : IMessage
    {
        Task<Result<TOutput>> Handle(TInput message);
    }
}
