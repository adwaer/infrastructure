namespace In.Cqrs
{
    public interface IMsgHandler<in T> where T: IMessage
    {
        string Handle(T message);
    }
}
