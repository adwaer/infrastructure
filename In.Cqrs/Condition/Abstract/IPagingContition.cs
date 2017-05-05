namespace In.Cqrs.Condition.Abstract
{
    public interface IPagingContition
    {
        int Count { get; set; }
        int Page { get; set; }
    }
}