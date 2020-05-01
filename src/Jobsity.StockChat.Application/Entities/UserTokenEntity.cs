using Jobsity.StockChat.Domain.Types;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Entities
{
    public class UserTokenEntity : UserToken
    {
        public UserTokenEntity() 
        {
            User = new UserEntity();
        }

        public virtual UserEntity User { get; set; }
    }
}
