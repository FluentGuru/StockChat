using Jobsity.StockChat.Application.Commands;
using Jobsity.StockChat.Domain.Services;
using Jobsity.StockChat.Domain.Types;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Application.Handlers
{
    public class GetUserCommandHandler : IRequestHandler<GetUserCommand, User>
    {
        private readonly IDataSource dataSource;

        public GetUserCommandHandler(IDataSource dataSource)
        {
            this.dataSource = dataSource;
        }

        public async Task<User> Handle(GetUserCommand request, CancellationToken cancellationToken)
        {
            return await dataSource.GetUser(request.Nickname);
        }
    }
}
