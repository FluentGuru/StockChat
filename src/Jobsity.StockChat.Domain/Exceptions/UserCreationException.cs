using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Jobsity.StockChat.Domain.Exceptions
{
    public class UserCreationException : Exception
    {
        public UserCreationException()
        {
        }

        public UserCreationException(string message) : base(message)
        {
        }

        public UserCreationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserCreationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
