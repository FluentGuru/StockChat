using Jobsity.StockChat.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Application.Services
{
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
