using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Jobsity.StockChat.Domain.Exceptions
{
    public class TokenNotFoundException : Exception
    {
        public TokenNotFoundException()
        {
        }

        public TokenNotFoundException(string message) : base(message)
        {
        }

        public TokenNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TokenNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
