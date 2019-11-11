namespace In.Web.Middlerwares
{
    /// <summary>
    /// Unhandled excpetions wrapper
    /// </summary>
    public class MiddlewareExceptionWrapper
    {
        /// <summary>
        /// Messgae
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="message"></param>
        public MiddlewareExceptionWrapper(string message)
        {
            Message = message;
        }
    }
}