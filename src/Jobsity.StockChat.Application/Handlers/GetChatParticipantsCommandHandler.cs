using Jobsity.StockChat.Application.Commands;
using Jobsity.StockChat.Domain.Entities;
using Jobsity.StockChat.Domain.Services;
using Jobsity.StockChat.Domain.Types;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Application.Handlers
{
    public class GetChatParticipantsCommandHandler : IRequestHandler<GetChatParticipantsCommand, IEnumerable<ChatParticipant>>
    {
        private readonly IDataSource dataSource;

        public GetChatParticipantsCommandHandler(IDataSource dataSource)
        {
            this.dataSource = dataSource;
        }

        public async Task<IEnumerable<ChatParticipant>> Handle(GetChatParticipantsCommand request, CancellationToken cancellationToken)
        {
            return await dataSource.FetchAsync<ChatParticipantEntity, ChatParticipant>(
                participants => participants
                .Where(
                    p => p.Stock == request.Stock)
                .OrderBy(
                    p => p.Nickname));
        }
    }
}
