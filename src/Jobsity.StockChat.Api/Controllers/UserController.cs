using Jobsity.StockChat.Api.Constants;
using Jobsity.StockChat.Api.Filters;
using Jobsity.StockChat.Application.Commands;
using Jobsity.StockChat.Application.Constants;
using Jobsity.StockChat.Domain.Exceptions;
using Jobsity.StockChat.Domain.Types;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Api.Controllers
{
    [ApiController]
    [Route("/api/users")]
    public class UserController : Controller
    {
        private readonly IMediator mediator;
        private readonly ILogger<UserController> logger;

        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            this.mediator = mediator;
            this.logger = logger;
        }

        [HttpGet]
        [Route("{nickname}")]
        public async Task<ActionResult<User>> GetUser([FromRoute]string nickname)
        {
            var user = await mediator.Send(new GetUserCommand(nickname));
            if(user != null)
            {
                return user;
            }

            return NotFound();
        }

        [HttpPost]
        [Route("auth")]
        public async Task<ActionResult<UserAuthentication>> SignIn([FromBody]string nickname, [FromBody]string password)
        {
            try
            {
                return await AuthenticateUser(nickname, password);
            }
            catch (UserAuthenticationFailedException)
            {
                return Unauthorized();
            }
        }

        [HttpDelete]
        [Route("auth")]
        [StockAuthorize]
        public async Task<ActionResult> SignOut([FromHeader(Name = AuthenticationConstants.AuthToken)]string authToken)
        {
            try
            {
                await mediator.Send(new EndAuthorizationCommand(authToken));
            }
            catch(TokenNotFoundException tfex)
            {
                logger.LogError(tfex.Message, tfex);
            }

            return Ok();
        }

        [HttpPost]
        [Route("")]
        public async Task<ActionResult<UserAuthentication>> CreateUser([FromBody]string nickname, [FromBody]string password)
        {
            try
            {
                await mediator.Send(new CreateUserCommand(nickname, password));
                return await AuthenticateUser(nickname, password);
            }
            catch (UserAuthenticationFailedException)
            {
                return Unauthorized();
            }
            catch(UserCreationException ucex)
            {
                logger.LogError(ucex.Message, ucex);
                return StatusCode(500);
            }
        }

        private async Task<ActionResult<UserAuthentication>> AuthenticateUser(string nickname, string password)
        {
            return await mediator.Send(new AuthenticateUserCommand(nickname, password, UserAuthConstants.TokenExpirationTime));
        }
    }
}
