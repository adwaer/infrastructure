namespace In.Cqrs
{
    public interface IMessageResult : IMessage
    {
        string Body { get; set; }
        string Info { get; set; }
        bool Socceed { get; set; }
        string Type { get; set; }
    }
}