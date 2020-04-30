using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Domain.Services
{
    public interface IHasher
    {
        string Generate(string source, string salt);
    }
}
