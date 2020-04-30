using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Domain.Types
{
    public class Chat
    {
        public string Stock { get; set; }

        public DateTime CreateDate { get; set; }

        public string OwnerNickname { get; set; }
    }
}
