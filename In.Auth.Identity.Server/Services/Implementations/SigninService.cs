using System.Threading.Tasks;
using In.Common.Exceptions;
using In.DataMapping;
using Microsoft.AspNetCore.Identity;

namespace In.Auth.Identity.Server.Services.Implementations
{
    public class SigninService<TUser> : ISigninService where TUser : class
    {
        private readonly SignInManager<TUser> _signInManager;
        private readonly UserManager<TUser> _userManager;
        private readonly IMapperService _mapperService;

        public SigninService(SignInManager<TUser> signInManager, UserManager<TUser> userManager, IMapperService mapperService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapperService = mapperService;
        }

        public async Task<TResult> ByEmail<TResult>(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                throw new AuthException("User with such mail not found");
            }

            var result = await _signInManager.PasswordSignInAsync(user, password, true, false);

            if (result.IsLockedOut)
                throw new AuthException("User is blocked");

            if (!result.Succeeded)
                throw new AuthException("Incorrect password");

            return _mapperService.GetFrom<TUser, TResult>(user);
        }
    }
}