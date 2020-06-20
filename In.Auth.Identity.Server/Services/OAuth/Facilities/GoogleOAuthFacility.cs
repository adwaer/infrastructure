using System.Threading.Tasks;
using Google.Apis.Auth;
using In.Auth.Identity.Server.Services.Exception;

namespace In.Auth.Identity.Server.Services.OAuth.Facilities
{
    public class GoogleOAuthFacility : IOAuthFacility
    {
        public async Task<OAuthPayload> GetData(string oAuthToken)
        {
            var googlePayload = await GoogleJsonWebSignature.ValidateAsync(oAuthToken);
            if (googlePayload == null)
            {
                throw new AuthException("Google token invalid");
            }

            return new OAuthPayload(googlePayload.Email, googlePayload.EmailVerified,
                googlePayload.GivenName,
                googlePayload.FamilyName, googlePayload.Subject);
        }
    }
}