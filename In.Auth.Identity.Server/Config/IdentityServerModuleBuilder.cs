using System;
using In.AppBuilder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace In.Auth.Identity.Server.Config
{
    public class IdentityServerModuleBuilder<TUser, TCtx> : CoreModuleBuilder
        where TUser : IdentityUser where TCtx : DbContext
    {
        private readonly Action<DbContextOptionsBuilder> _optionsBuilder;

        public IdentityServerModuleBuilder(IServiceCollection services, Action<DbContextOptionsBuilder> optionsBuilder)
            : base(services)
        {
            _optionsBuilder = optionsBuilder;
        }

        public override IServiceCollection AddServices()
        {
            return Collection.AddIdentityServer<TUser, TCtx>()
                .AddDbContext<TCtx>(_optionsBuilder);
        }
    }
}