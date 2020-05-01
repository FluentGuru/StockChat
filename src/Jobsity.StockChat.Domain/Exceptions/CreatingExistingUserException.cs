using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Jobsity.StockChat.Domain.Exceptions
{
    public class CreatingExistingUserException : UserCreationException
    {
        public CreatingExistingUserException()
        {
        }

        public CreatingExistingUserException(string message) : base(message)
        {
        }

        public CreatingExistingUserException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CreatingExistingUserException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
