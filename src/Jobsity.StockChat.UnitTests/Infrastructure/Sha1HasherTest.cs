﻿using Jobsity.StockChat.Application.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Jobsity.StockChat.UnitTests.Infrastructure
{
    public class Sha1HasherTest
    {
        [Test]
        public void ShouldReturnSameHashWhenGivenSameSourceAndSalt()
        {
            var hasher = new Sha1Hasher();
            var hash = hasher.Generate("TEST", "SALT");

            Assert.AreEqual("TEgfK06VHp44cC+mq+SkeTvg9Cg=", hash);
        }

        [Test]
        public void ShouldReturnSaltWithSpecifiedLength()
        {
            var hasher = new Sha1Hasher();
            Assert.AreEqual(8, hasher.GetSalt(8).Length);
        }
    }
}
