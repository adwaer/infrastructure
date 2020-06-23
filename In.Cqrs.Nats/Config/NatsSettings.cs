namespace In.Cqrs.Nats.Config
{
    /// <summary>
    /// Nats options contract
    /// </summary>
    public class NatsSettings
    {
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Nats queue user name
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// Nats queue password
        /// </summary>
        public string Password { get; set; }
    }
}