using System.Threading.Tasks;

namespace In.Auth.Identity.Server.Services.OAuth
{
    public interface IOAuthFacility
    {
        Task<OAuthPayload> GetData(string oAuthToken);
    }
}