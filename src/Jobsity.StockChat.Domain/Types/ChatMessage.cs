using Jobsity.StockChat.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Domain.Types
{
    public class ChatMessage
    {
        public string Stock { get; set; }

        public string FromNickName { get; set; }

        public MessageTypes Type { get; set; }

        public string Content { get; set; }
    }
}
