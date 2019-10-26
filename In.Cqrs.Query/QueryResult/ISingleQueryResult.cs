namespace In.Cqrs.Query.QueryResult
{
    public interface ISingleQueryResult<T>
    {
        T Data { get; set; }
    }

    public class SingleQueryResult<T> : ISingleQueryResult<T>
    {
        public T Data { get; set; }
    }
}
