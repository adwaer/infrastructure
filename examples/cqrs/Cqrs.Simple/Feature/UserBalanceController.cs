using System.Threading.Tasks;
using Cqrs.Domain.Features.Ordering.Commands;
using Cqrs.Domain.Features.Ordering.Criteria;
using Cqrs.Domain.Features.Ordering.QueryResult;
using In.Cqrs.Command;
using In.Cqrs.Query.Queries;
using In.FunctionalCSharp;
using Microsoft.AspNetCore.Mvc;

namespace Cqrs.Simple.Feature
{
    /// <summary>
    /// Operations with user balance
    /// </summary>
    [ApiController]
    [Route("user/{id}/balance")]
    public class UserBalanceController : ControllerBase
    {
        private readonly IQueryBuilder _queryBuilder;
        private readonly IMessageSender _messageSender;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="queryBuilder"></param>
        /// <param name="messageSender"></param>
        public UserBalanceController(IQueryBuilder queryBuilder, IMessageSender messageSender)
        {
            _queryBuilder = queryBuilder;
            _messageSender = messageSender;
        }

        /// <summary>
        /// returns current user balance
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            var balanceResponse = await _queryBuilder
                .For<UserBalanceQueryResult>()
                .WithAsync(new UserBalanceCriterion(id));

            return Ok(balanceResponse.Balance);
        }

        /// <summary>
        /// Change users balance
        /// </summary>
        /// <param name="id">user id</param>
        /// <param name="amount">amount</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(int id, [FromForm] decimal amount)
        {
            var result = await ChangeBalanceCmd.Create(id, amount)
                .Tap(cmd => _messageSender.SendAsync(cmd));

            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return NoContent();
        }
    }
}