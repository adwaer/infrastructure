using CSharpFunctionalExtensions;
using In.Cqrs.Command;
using In.FunctionalCSharp;

namespace Cqrs.Domain.Commands
{
    public class TestCmd : IMessage
    {
        public int SomeVal { get; }
        public string SomeText { get; }

        private TestCmd(int someVal, string someText)
        {
            SomeVal = someVal;
            SomeText = someText;
        }

        public static Result<TestCmd> Create(int someVal, string someText)
        {
            return ParametersValidation.Validate(
                    ParametersValidation.NotDefaultValue(someVal, nameof(someVal)),
                    ParametersValidation.NotNullOrWhiteSpace(someText, nameof(someText))
                )
                .OnAllSuccess(() => new TestCmd(someVal, someText));
        }
    }
}
