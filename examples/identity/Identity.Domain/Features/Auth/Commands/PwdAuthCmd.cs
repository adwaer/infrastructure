using In.Cqrs.Command;
using In.FunctionalCSharp;

namespace Identity.Domain.Features.Auth.Commands
{
    public class PwdAuthCmd : IMessage
    {
        public string Password { get; }
        public string Email { get; }
        
        private PwdAuthCmd(string password, string email)
        {
            Password = password;
            Email = email;
        }
        
        public static Result<PwdAuthCmd> Create(string password, string email)
        {
            return ParametersValidation.Validate(
                    ParametersValidation.NotNullOrWhiteSpace(password, nameof(password)),
                    ParametersValidation.NotNullOrWhiteSpace(email, nameof(email))
                )
                .Combine()
                .Map(() => new PwdAuthCmd(password, email));
        }
    }
}