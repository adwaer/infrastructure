using System.Threading.Tasks;
using Identity.Domain.Features.Profile.Specifications;
using Identity.Domain.Models;
using Identity.Simple.Models.Response;
using In.Auth.Services;
using In.Cqrs.Query.Queries;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Simple.Controllers
{
    /// <summary>
    /// Controller API for users profile
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProfileController : ControllerBase
    {
        private readonly IQueryBuilder _queryBuilder;
        private readonly IUserContextService _contextService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="queryBuilder"></param>
        /// <param name="contextService"></param>
        public ProfileController(IQueryBuilder queryBuilder, IUserContextService contextService)
        {
            _queryBuilder = queryBuilder;
            _contextService = contextService;
        }

        /// <summary>
        /// Get email by id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        [ProducesResponseType(401)]
        [ProducesResponseType(404, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> GetEmail()
        {
            var user = await _queryBuilder
                .ForGeneric<User>()
                .Where(UserSpecifications.WithId(_contextService.GetUserId(null).ToString()))
                .FirstOrDefaultAsync();

            return Ok(user.Email);
        }
    }
}
