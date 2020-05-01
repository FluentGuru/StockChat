using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Domain.Types
{
    public class ChatMessage
    {
        public string Stock { get; set; }

        public string FromNickName { get; set; }

        public string Message { get; set; }

        public DateTime SentTime { get; set; }
    }
}
