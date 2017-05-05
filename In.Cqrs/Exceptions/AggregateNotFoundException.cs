using System;

namespace In.Cqrs.Exceptions
{
    public class AggregateNotFoundException : Exception
    {
        public AggregateNotFoundException()
        {
        }

        public AggregateNotFoundException(string message)
        : base(message)
        {
        }

        public AggregateNotFoundException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}