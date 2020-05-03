using Jobsity.StockChat.Domain.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Domain.Entities
{
    public class ChatParticipantEntity : ChatParticipant, IEntity
    {
        public ChatParticipantEntity()
        {
            Chat = new ChatEntity();
            Participant = new UserEntity();
        }

        public virtual ChatEntity Chat { get; set; }

        public virtual UserEntity Participant { get; set; }
    }
}
