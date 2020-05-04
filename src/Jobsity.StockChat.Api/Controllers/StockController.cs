using Jobsity.StockChat.Api.Filters;
using Jobsity.StockChat.Application.Commands;
using Jobsity.StockChat.Domain.Constants;
using Jobsity.StockChat.Domain.Types;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Api.Controllers
{
    [ApiController]
    [Route("/api/stocks")]
    public class StockController : Controller
    {
        private readonly IMediator mediator;

        public StockController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        [StockAuthorize]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Chat>>> GetAllChats()
        {
            return Ok(await mediator.Send(new GetAllChatsCommand()));
        }

        [HttpGet]
        [StockAuthorize]
        [Route("{stock}")]
        public async Task<ActionResult<Chat>> JoinChat([FromRoute]string stock, [FromHeader]string nickname)
        {
            return (Chat)await mediator.Send(new JoinChatCommand(nickname, stock));
        }

        [HttpGet]
        [StockAuthorize]
        [Route("{stock}/messages")]
        public async Task<ActionResult<IEnumerable<ChatMessage>>> GetMessages([FromRoute]string stock)
        {
            return (await mediator.Send(new GetChatMessagesCommand(stock, ChatConstants.FetchMessagesCount))) as ActionResult<IEnumerable<ChatMessage>>;
        }

        [HttpPost]
        [StockAuthorize]
        [Route("{stock}/messages")]
        public async Task<ActionResult> SendMessage([FromRoute]string stock, [FromHeader]string nickname, [FromBody] string message)
        {
            await mediator.Send(new SendMessageCommand(message, nickname, stock));
            return Ok();
        }

        [HttpGet]
        [StockAuthorize]
        [Route("{stock}/participants")]
        public async Task<ActionResult<IEnumerable<ChatParticipant>>> GetParticipants([FromRoute] string stock)
        {
            return (await mediator.Send(new GetChatParticipantsCommand(stock))) as ActionResult<IEnumerable<ChatParticipant>>;
        }
    }
}
