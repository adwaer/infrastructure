using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using In.Auth.Config;
using Microsoft.IdentityModel.Tokens;

namespace In.Auth.Identity.Server.Services.Implementations
{
    /// <summary>
    /// Service for working with tokens.
    /// </summary>
    public class TokenService : ITokenService
    {
        /// <inheritdoc />
        public string GenerateToken(AuthenticationSettings options, (string id, string name) user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.name),
                new Claim(ClaimsHelper.ClaimUserId, user.id)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(options.SecretJwtKey));
            var jwt = new JwtSecurityToken(
                options.Url,
                options.Url,
                notBefore: DateTime.UtcNow,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromHours(options.LifeTime)),
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}