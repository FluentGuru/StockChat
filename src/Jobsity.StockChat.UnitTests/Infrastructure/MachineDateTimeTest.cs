using Jobsity.StockChat.Application.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.UnitTests.Infrastructure
{
    public class MachineDateTimeTest
    {
        [Test]
        public void ShouldReturnSystemDate()
        {
            Assert.AreEqual(DateTime.Now.Month, new MachineDateTime().Now.Month);
        }
    }
}
