using Jobsity.StockChat.Application.Commands;
using Jobsity.StockChat.Domain.Entities;
using Jobsity.StockChat.Domain.Exceptions;
using Jobsity.StockChat.Domain.Services;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Application.Handlers
{
    public class EndAuthorizationCommandHandler : IRequestHandler<EndAuthorizationCommand>
    {
        private readonly IUnitOfWork unitOfWork;

        public EndAuthorizationCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(EndAuthorizationCommand request, CancellationToken cancellationToken)
        {
            var token = await unitOfWork.GetSingleAsync<UserTokenEntity>(t => t.Token == request.Token);
            if(token != null)
            {
                token.ExpirationDate = DateTime.MinValue;
                await unitOfWork.UpdateAndSaveAsync(token);
                return Unit.Value;
            }

            throw new TokenNotFoundException("Invalid token or authorization");
        }
    }
}
