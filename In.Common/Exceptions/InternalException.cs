using System;

namespace In.Common.Exceptions
{
    public class InternalException : System.Exception
    {
        public InternalException(string message) : base(message)
        {
        }

        public InternalException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}