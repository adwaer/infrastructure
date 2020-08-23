using Identity.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Dal
{
    public class IdentityCtx : IdentityDbContext<User, Role, string>
    {
        public DbSet<SimpleMessageResult> MessageHistories { get; set; }

        public IdentityCtx(DbContextOptions options) : base(options)
        {
        }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(entity =>
            {
                entity.ToTable(name: "Users");
                entity.Ignore(user => user.TwoFactorEnabled);
                entity.Ignore(user => user.ConcurrencyStamp);
            });

            builder.Entity<Role>(entity => { entity.ToTable(name: "Roles"); });
            
        }
    }
}