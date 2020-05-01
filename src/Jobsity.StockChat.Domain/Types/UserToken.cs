using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Domain.Types
{
    public class UserToken
    {
        public string Token { get; set; }
        public string Nickname { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
