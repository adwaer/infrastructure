using In.Cqrs.Command;
using In.FunctionalCSharp;

namespace Cqrs.Domain.Features.Ordering.Commands
{
    public class ChangeBalanceCmd : IMessage
    {
        public int UserId { get; }
        public decimal Amount { get; }

        private ChangeBalanceCmd(int userId, decimal amount)
        {
            UserId = userId;
            Amount = amount;
        }

        public static Result<ChangeBalanceCmd> Create(int userId, decimal amount)
        {
            return ParametersValidation.Validate(
                    ParametersValidation.Ensure(() => userId > 0, "Invalid user"),
                    ParametersValidation.NotDefaultValue(amount, nameof(amount))
                )
                .Combine()
                .Map(() => new ChangeBalanceCmd(userId, amount));
        }
    }
}
