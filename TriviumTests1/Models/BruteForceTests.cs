using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trivium.Models;

namespace TriviumTests1.Models
{
    [TestClass]
    public class BruteForceTests
    {
        [TestMethod()]
        public void GetAllCombinationTest()
        {
            var bytes = new byte[] { 1 };
            var bits = new BitArray(bytes);
            Assert.IsTrue(bits.Get(0));
        }
    }
}