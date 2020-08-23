using System.Linq;
using System.Threading.Tasks;
using Identity.Domain.Features.Registration.Commands;
using Identity.Domain.Models;
using In.Cqrs.Command;
using In.FunctionalCSharp;
using Microsoft.AspNetCore.Identity;

namespace Identity.CommandHandlers
{
    public class RegistrationCommandHandler : ICommandHandler<PwdRegistrationCmd>
    {
        private readonly UserManager<User> _userManager;

        public RegistrationCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> Handle(PwdRegistrationCmd message)
        {
            var user = await _userManager.FindByEmailAsync(message.Email);
            if (user != null)
                return Result.Failure("Email is already registered");

            user = new User
            {
                Email = message.Email,
                UserName = message.Email,
            };

            var result = await _userManager.CreateAsync(user, message.Password);
            return !result.Succeeded
                ? Result.Failure(result.Errors.First().Description)
                : Result.Success();
        }
    }
}