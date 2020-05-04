using Jobsity.StockChat.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.UnitTests.Commons
{
    public class TransientDateTime : IDateTime
    {
        public TransientDateTime()
        {
            Now = DateTime.Now;
        }

        public DateTime Now { get; set; }
    }
}
