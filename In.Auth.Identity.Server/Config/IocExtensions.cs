using In.Auth.Identity.Server.Services;
using In.Auth.Identity.Server.Services.Implementations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace In.Auth.Identity.Server.Config
{
    public static class IocExtensions
    {
        public static IServiceCollection AddIdentityServer<TUser, TCtx>(this IServiceCollection services,
            (
                ClaimsIdentityOptions,
                LockoutOptions,
                PasswordOptions,
                StoreOptions,
                SignInOptions,
                TokenOptions,
                UserOptions
                ) tuple = default)
            where TUser : IdentityUser where TCtx : DbContext
        {
            var (claimsIdentityOptions, lockoutOptions, passwordOptions, storeOptions, signInOptions, tokenOptions,
                userOptions) = tuple;

            services.AddIdentity<TUser, IdentityRole>(options =>
                {
                    if (claimsIdentityOptions != null) options.ClaimsIdentity = claimsIdentityOptions;
                    if (lockoutOptions != null) options.Lockout = lockoutOptions;
                    if (passwordOptions != null) options.Password = passwordOptions;
                    if (storeOptions != null) options.Stores = storeOptions;
                    if (signInOptions != null) options.SignIn = signInOptions;
                    if (tokenOptions != null) options.Tokens = tokenOptions;
                    if (userOptions != null) options.User = userOptions;
                })
                .AddEntityFrameworkStores<TCtx>()
                .AddDefaultTokenProviders();

            return services
                .AddTransient<ITokenService, TokenService>()
                .AddTransient<ISigninService, SigninService<TUser>>();
        }
    }
}