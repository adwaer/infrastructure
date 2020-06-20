namespace In.Auth.Identity.Server.Services.OAuth
{
    /// <summary>
    /// Oauth data
    /// </summary>
    public class OAuthPayload
    {
        public string Email { get; } 
        public bool EmailConfirmed { get; } 
        public string FirstName { get; } 
        public string LastName { get; } 
        public string ExternalId { get; }

        public OAuthPayload(string email, bool emailConfirmed, string firstName, string lastName, string externalId)
        {
            Email = email ?? externalId;
            EmailConfirmed = emailConfirmed;
            FirstName = firstName;
            LastName = lastName;
            ExternalId = externalId;
        }
    }
}