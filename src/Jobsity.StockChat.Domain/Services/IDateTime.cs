﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.Domain.Services
{
    public interface IDateTime
    {
        DateTime Now { get; }
    }
}
