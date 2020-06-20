using System.Threading.Tasks;

namespace In.Auth.Identity.Server.Services
{
    public interface ISigninService
    {
        Task<TResult> ByEmail<TResult>(string email, string password);
    }
}