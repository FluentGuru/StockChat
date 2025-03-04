﻿using Jobsity.StockChat.Domain.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Domain.Entities
{
    public class UserEntity : User, IEntity
    {
        public UserEntity()
        {
            Participations = new List<ChatParticipantEntity>();
            Messages = new List<ChatMessageEntity>();
            UserTokens = new List<UserTokenEntity>();
        }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public virtual ICollection<ChatParticipantEntity> Participations { get; set; }

        public virtual ICollection<ChatMessageEntity> Messages { get; set; }

        public virtual ICollection<UserTokenEntity> UserTokens { get; set; }
    }
}
