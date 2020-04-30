using Jobsity.StockChat.Domain.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Entities
{
    public class ChatParticipantEntity : ChatParticipant
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
