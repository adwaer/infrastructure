using System;

namespace In.Common.Exceptions
{
    public class BadRequestException : Exception
    {
        private readonly string _paramName;

        public BadRequestException(string message) : base(message)
        {
        }

        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public BadRequestException(string paramName, string message) : base(message)
        {
            _paramName = paramName;
        }

        public override string Message
        {
            get
            {
                string s = base.Message;
                if (!string.IsNullOrEmpty(_paramName))
                {
                    s = $"{s} param name: {_paramName}";
                }

                return s;
            }
        }
    }
}