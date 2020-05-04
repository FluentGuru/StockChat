using Jobsity.StockChat.Application.Commands;
using Jobsity.StockChat.Domain.Entities;
using Jobsity.StockChat.Domain.Services;
using Jobsity.StockChat.Domain.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Jobsity.StockChat.Application.Handlers
{
    public class GetAllChatsCommandHandler : IRequestHandler<GetAllChatsCommand, IEnumerable<Chat>>
    {
        private readonly IDataSource dataSource;

        public GetAllChatsCommandHandler(IDataSource dataSource)
        {
            this.dataSource = dataSource;
        }

        public Task<IEnumerable<Chat>> Handle(GetAllChatsCommand request, CancellationToken cancellationToken)
        {
            return dataSource.FetchAsync<ChatEntity, Chat>(chats => chats);
        }
    }
}
