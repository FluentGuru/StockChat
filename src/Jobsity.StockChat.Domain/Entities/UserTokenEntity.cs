using Jobsity.StockChat.Domain.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Domain.Entities
{
    public class UserTokenEntity : UserToken, IEntity
    {
        public UserTokenEntity() 
        {
        }

        public virtual UserEntity User { get; set; }
    }
}
