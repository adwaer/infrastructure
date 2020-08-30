using System.Threading.Tasks;
using Identity.Domain.Features.Auth.Commands;
using Identity.Domain.Models;
using In.Auth.Config;
using In.Auth.Identity.Server.Services;
using In.Cqrs.Command;
using In.FunctionalCSharp;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Identity.CommandHandlers
{
    public class AuthCommandHandler : ICommandHandler<PwdAuthCmd, string>
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly AuthenticationSettings _authenticationSettings;

        public AuthCommandHandler(UserManager<User> userManager, ITokenService tokenService,
            IOptions<AuthenticationSettings> authenticationSettings)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _authenticationSettings = authenticationSettings.Value;
        }

        public async Task<Result<string>> Handle(PwdAuthCmd message)
        {
            var user = await _userManager.FindByEmailAsync(message.Email);
            if (user == null)
                return Result.Failure<string>("Auth failed");

            var token = _tokenService.GenerateToken(_authenticationSettings, (user.Id, user.UserName));
            return Result.Success(token);
        }
    }
}