using System;

namespace In.FunctionalCSharp
{
    public class ResultSuccessException : Exception
    {
        internal ResultSuccessException()
            : base(Result.Messages.ErrorIsInaccessibleForSuccess)
        {
        }
    }
}
