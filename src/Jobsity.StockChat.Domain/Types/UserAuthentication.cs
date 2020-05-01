using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Domain.Types
{
    public class UserAuthentication
    {
        public User User { get; set; }

        public string Token { get; set; }
    }
}
