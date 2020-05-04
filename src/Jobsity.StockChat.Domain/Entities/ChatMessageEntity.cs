using Jobsity.StockChat.Domain.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Domain.Entities
{
    public class ChatMessageEntity : ChatMessage, IEntity
    {
        public ChatMessageEntity()
        {
        }

        public int Id { get; set; }

        public virtual ChatEntity Chat { get; set; }

        public virtual UserEntity Sender { get; set; }
    }
}
