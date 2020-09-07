using System;
using System.Linq;
using System.Security.Claims;
using In.Auth.Config;
using In.Common.Exceptions;
using Microsoft.AspNetCore.Http;

namespace In.Auth.Services.Implementations
{
    /// <summary>
    /// Service for received user info from context.
    /// </summary>
    public class UserContextService : IUserContextService
    {
        private const string AuthTokenHeader = "Authorization";

        private readonly HttpContext _httpContext;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContext = httpContextAccessor.HttpContext;
        }

        /// <inheritdoc />
        public string GetUserId(string userIdClaim = ClaimsHelper.ClaimUserId)
        {
            var claimWithUserId = _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == userIdClaim);

            if (claimWithUserId == null)
                throw new AuthException("Current user unauthorized.");

            return claimWithUserId.Value;
        }

        /// <inheritdoc />
        public string GetUserEmail()
        {
            var claimWithUserEmail =
                _httpContext.User.Claims.FirstOrDefault(claim => claim.Type == ClaimsIdentity.DefaultNameClaimType);

            if (claimWithUserEmail == null)
                throw new AuthException("Current user unauthorized.");

            return claimWithUserEmail.Value;
        }

        public string GetAccessToken(string headerName = AuthTokenHeader)
        {
            return _httpContext.Request.Headers[headerName];
        }
    }
}