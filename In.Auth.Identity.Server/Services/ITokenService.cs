using In.Auth.Config;
using Microsoft.AspNetCore.Authentication;

namespace In.Auth.Identity.Server.Services
{
    /// <summary>
    /// Interface for working with tokens.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generate access token.
        /// </summary>
        /// <param name="user">user model.</param>
        /// <param name="options">Authentication options</param>
        /// <returns>Access token as string.</returns>
        string GenerateToken(AuthenticationSettings options, (string id, string name) user);
    }
}