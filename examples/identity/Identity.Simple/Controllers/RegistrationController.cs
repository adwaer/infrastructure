using System;
using System.Threading.Tasks;
using Identity.Domain.Features.Registration.Commands;
using Identity.Simple.Models;
using Identity.Simple.Models.Response;
using In.Cqrs.Command;
using In.FunctionalCSharp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Simple.Controllers
{
    /// <summary>
    /// Controller API for users registration
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class RegistrationController : ControllerBase
    {
        private readonly IMessageSender _messageSender;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="messageSender"></param>
        public RegistrationController(IMessageSender messageSender)
        {
            _messageSender = messageSender;
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="request">Query model 'Create account'.</param>
        /// <returns>Status code.</returns>
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400, Type = typeof(ErrorResponse))]
        [ProducesResponseType(401)]
        [ProducesResponseType(404, Type = typeof(ErrorResponse))]
        public async Task<ActionResult> CreateUser([FromBody] CreateAccountRequest request)
        {
            if (!Request.Headers.ContainsKey("Origin"))
            {
                throw new AccessViolationException();
            }

            var result = await PwdRegistrationCmd
                .Create(request.Password, request.ConfirmPassword, request.Email)
                .Bind(async cmd => await _messageSender.SendAsync(cmd));

            if (result.IsFailure)
            {
                throw new InvalidOperationException(result.Error);
            }

            return NoContent();
        }
    }
}
