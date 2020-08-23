using In.Cqrs.Command;
using In.FunctionalCSharp;
using Web.Infrastructure;

namespace Identity.Domain.Features.Registration.Commands
{
    public class PwdRegistrationCmd : IMessage
    {
        public string Password { get; }
        public string Email { get; }

        private PwdRegistrationCmd(string password, string email)
        {
            Password = password;
            Email = email;
        }

        public static Result<PwdRegistrationCmd> Create(string password, string passwordConfirm, string email)
        {
            return ParametersValidation.Validate(
                    ParametersValidation.NotNullOrWhiteSpace(password, nameof(password)),
                    ParametersValidation.Ensure(() => password == passwordConfirm, nameof(passwordConfirm)),
                    ParametersValidation.NotNullOrWhiteSpace(email, nameof(email)),
                    ParametersValidation.Ensure(() => new EmailAttribute().IsValid(email), nameof(email))
                )
                .Combine()
                .Map(() => new PwdRegistrationCmd(password, email));
        }
    }
}