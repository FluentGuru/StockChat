using Jobsity.StockChat.Application.Commands;
using Jobsity.StockChat.Domain.Entities;
using Jobsity.StockChat.Domain.Services;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Application.Handlers
{
    public class CheckAuthorizationCommandHandler : IRequestHandler<CheckAuthorizationCommand, bool>
    {
        private readonly IDataSource dataSource;
        private readonly IDateTime dateTime;

        public CheckAuthorizationCommandHandler(IDataSource dataSource, IDateTime dateTime)
        {
            this.dataSource = dataSource;
            this.dateTime = dateTime;
        }

        public async Task<bool> Handle(CheckAuthorizationCommand request, CancellationToken cancellationToken)
        {
            var now = dateTime.Now;
            return await dataSource.AnyAsync<UserTokenEntity>(t => t.Token == request.Token && t.ExpirationDate > now);
        }
    }
}
