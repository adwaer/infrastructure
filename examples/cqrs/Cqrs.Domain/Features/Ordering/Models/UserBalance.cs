using In.DataAccess.Entity;
using In.DDD;

namespace Cqrs.Domain.Features.Ordering.Models
{
    public class UserBalance : HasKey
    {
        public int UserId { get; set; }
        public decimal Balance { get; set; }

        public UserBalance()
        {
            Balance = 0;
        }
    }
}