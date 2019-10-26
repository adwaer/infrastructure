namespace In.Logging
{
    public interface IRequestCorrelationIdentifier
    {
        string CorrelationId { get; }
    }
}