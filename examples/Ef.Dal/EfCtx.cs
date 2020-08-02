using Cqrs.Domain.Features.Ordering.Models;
using Cqrs.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Ef.Dal
{
    public class EfCtx : DbContext
    {
        public EfCtx(DbContextOptions options) : base(options)
        {
        }

        public DbSet<UserBalance> UserBalances { get; set; }
        public DbSet<SimpleMessageResult> SimpleMessageResults { get; set; }
    }
}