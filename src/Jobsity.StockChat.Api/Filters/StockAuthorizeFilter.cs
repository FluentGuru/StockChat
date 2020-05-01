using Jobsity.StockChat.Api.Constants;
using Jobsity.StockChat.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Api.Filters
{
    public class StockAuthorizeAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var token = context.HttpContext.Request.Headers[AuthenticationConstants.AuthToken];
            if (!string.IsNullOrEmpty(token))
            {
                var mediator = context.HttpContext.RequestServices.GetService<IMediator>();
                var authenticated = await mediator.Send(new CheckAuthorizationCommand(token));
                if (authenticated)
                {
                    await base.OnActionExecutionAsync(context, next);
                    
                }

                throw new UnauthorizedAccessException("Session expired");
            }

            throw new UnauthorizedAccessException("Not authenticated");
            
        }
    }
}
