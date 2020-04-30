using Jobsity.StockChat.Domain.Services;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Jobsity.StockChat.Application.Services
{
    public class Sha1Hasher : IHasher
    {
        public string Generate(string source, string salt)
        {
            using(var sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(source + salt));
                return Convert.ToBase64String(hash);
            }
        }
    }
}
