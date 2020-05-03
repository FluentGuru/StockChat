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
    public class GetChatMessagesCommandHandler : IRequestHandler<GetChatMessagesCommand, IEnumerable<ChatMessage>>
    {
        private readonly IDataSource dataSource;

        public GetChatMessagesCommandHandler(IDataSource dataSource)
        {
            this.dataSource = dataSource;
        }

        public async Task<IEnumerable<ChatMessage>> Handle(GetChatMessagesCommand request, CancellationToken cancellationToken)
        {
            return await dataSource.FetchAsync<ChatMessageEntity, ChatMessage>(messages => 
            messages.Where(
                m => m.Stock == request.Stock)
            .OrderByDescending(
                m => m.SentTime)
            .Take(request.Count));
        }
    }
}
