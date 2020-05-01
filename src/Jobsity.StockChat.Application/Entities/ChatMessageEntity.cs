using Jobsity.StockChat.Domain.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Entities
{
    public class ChatMessageEntity : ChatMessage
    {
        public ChatMessageEntity()
        {
            Chat = new ChatEntity();
            Sender = new UserEntity();
        }

        public int Id { get; set; }

        public virtual ChatEntity Chat { get; set; }

        public virtual UserEntity Sender { get; set; }
    }
}
