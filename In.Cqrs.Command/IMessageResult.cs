using In.DataAccess.Entity.Abstract;

namespace In.Cqrs.Command
{
    public interface IMessageResult : IMessage, IHasKey
    {
        string Body { get; set; }
        string Info { get; set; }
        bool Socceed { get; set; }
        string Type { get; set; }
    }
}