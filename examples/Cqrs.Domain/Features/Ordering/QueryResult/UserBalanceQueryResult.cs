namespace Cqrs.Domain.Features.Ordering.QueryResult
{
    public class UserBalanceQueryResult
    {
        public decimal Balance { get; set; }

        public UserBalanceQueryResult(decimal balance)
        {
            Balance = balance;
        }
    }
}