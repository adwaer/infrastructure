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
        private readonly (ClaimsIdentityOptions, LockoutOptions, PasswordOptions, StoreOptions, SignInOptions, TokenOptions, UserOptions) _options;

        public IdentityServerModuleBuilder(IServiceCollection services, Action<DbContextOptionsBuilder> optionsBuilder, (
            ClaimsIdentityOptions,
            LockoutOptions,
            PasswordOptions,
            StoreOptions,
            SignInOptions,
            TokenOptions,
            UserOptions
            ) options = default)
            : base(services)
        {
            _optionsBuilder = optionsBuilder;
            _options = options;
        }

        public override IServiceCollection AddServices()
        {
            return Collection.AddIdentityServer<TUser, TCtx>(_options)
                .AddDbContext<TCtx>(_optionsBuilder);
        }
    }
}