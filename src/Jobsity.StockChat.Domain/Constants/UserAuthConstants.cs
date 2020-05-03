using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Domain.Constants
{
    public static class UserAuthConstants
    {
        public const int PasswordSaltLength = 8;
        public static readonly TimeSpan TokenExpirationTime = TimeSpan.FromDays(1); 
    }
}
