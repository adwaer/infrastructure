using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace In.Web.Implementations
{
    /// <summary>
    /// Service for received user info from context.
    /// </summary>
    public class UserContextService : IUserContextService
    {
        private const string ClaimUserId = "UserId";

        private readonly HttpContext _httpContext;

        /// <inheritdoc />
        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        /// <inheritdoc />
        public Guid GetUserId()
        {
            var claimWithUserId = _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimUserId);

            if (claimWithUserId == null)
                throw new UnauthorizedAccessException("Current user unathorized.");

            return new Guid(claimWithUserId.Value);
        }

        /// <inheritdoc />
        public string GetUserEmail()
        {
            var claimWithUserEmail = _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimsIdentity.DefaultNameClaimType);

            if (claimWithUserEmail == null)
                throw new UnauthorizedAccessException("Current user unathorized.");

            return claimWithUserEmail.Value;
        }

        /// <inheritdoc />
        public string GetAccessToken()
        {
            throw new NotImplementedException();
//            return _httpContext.GetTokenAsync(JwtBearerDefaults.AuthenticationScheme, "access_token").GetAwaiter().GetResult();
        }
    }
}