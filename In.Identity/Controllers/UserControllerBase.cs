using System;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using In.Identity.ViewModel;
using In.Mapping;

namespace In.Identity.Controllers
{
    [AllowAnonymous]
    public class UserControllerBase<T, TViewModel> : ApiController where T : class, IUser<Guid> where TViewModel : IRegistrationModel
    {
        private readonly UserManager<T, Guid> _userManager;
        private readonly IMapperService<T, TViewModel> _mapperService;

        public UserControllerBase(UserManager<T, Guid> userManager, IMapperService<T, TViewModel> mapperService)
        {
            _userManager = userManager;
            _mapperService = mapperService;
        }

        public virtual async Task<IHttpActionResult> Get(string login, string password)
        {
            var result = await _userManager.FindAsync(login, password);
            if (result == null)
            {
                return BadRequest();
            }
            var identity = new GenericIdentity(login);
            var principal = new GenericPrincipal(identity, null);

            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }

            return Ok();
        }
        
        public virtual async Task<IHttpActionResult> Post(TViewModel id)
        {
            var account = _mapperService.GetFrom(id);
            if (await _userManager.FindByEmailAsync(id.Email) != null)
            {
                return BadRequest("username_busy");
            }

            var result = await _userManager
                .CreateAsync(account, id.Password);
            if (result == IdentityResult.Success)
            {
                await RegistrationCompleted(account.Id);
                return Ok(account.Id);
            }
            IHttpActionResult errorResult = GetErrorResult(result);
            if (errorResult != null)
            {
                return errorResult;
            }

            return BadRequest(result.ToString());
        }

        protected virtual Task RegistrationCompleted(Guid id)
        {
            return Task.FromResult<object>(null);
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }
    }
}
