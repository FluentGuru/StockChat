using Jobsity.StockChat.Domain.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Domain.Entities
{
    public class ChatEntity : Chat, IEntity
    {
        public ChatEntity()
        {
            Owner = new UserEntity();
            Participants = new List<ChatParticipantEntity>();
            Messages = new List<ChatMessageEntity>();
        }

        public virtual UserEntity Owner { get; set; }

        public virtual ICollection<ChatParticipantEntity> Participants { get; set; }

        public virtual ICollection<ChatMessageEntity> Messages { get; set; }
    }
}
