using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            var sut = new BruteForce(null, null, null);
            var all = sut.GetAllCombination().ToList();
        }
    }
}