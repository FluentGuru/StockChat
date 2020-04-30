using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Domain.Types
{
    public class User
    {
        public string Nickname { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime LastLoginDate { get; set; }
    }
}
