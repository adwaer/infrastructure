using System;
using System.Threading.Tasks;
using Identity.Domain.Features.Auth.Commands;
using Identity.Simple.Models;
using Identity.Simple.Models.Response;
using In.Cqrs.Command;
using In.FunctionalCSharp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Simple.Controllers
{
    /// <summary>
    /// Controller API for users auth
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IMessageSender _messageSender;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="messageSender"></param>
        public AuthController(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        /// <summary>
        /// Auth a new user.
        /// </summary>
        /// <param name="request">Query model 'Create account'.</param>
        /// <returns>Status code.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        [ProducesResponseType(401)]
        [ProducesResponseType(404, Type = typeof(ErrorResponse))]
        public async Task<ActionResult> ByPwd([FromBody] PwdAuthRequest request)
        {
            var result = await PwdAuthCmd
                .Create(request.Password, request.Email)
                .Bind(async cmd => await _messageSender.SendAsync(cmd));

            if (result.IsFailure)
            {
                throw new InvalidOperationException(result.Error);
            }

            return Ok(result);
        }
    }
}