using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Jobsity.StockChat.Domain.Exceptions
{
    public class UserAuthenticationFailedException : Exception
    {
        public UserAuthenticationFailedException()
        {
        }

        public UserAuthenticationFailedException(string message) : base(message)
        {
        }

        public UserAuthenticationFailedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserAuthenticationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
