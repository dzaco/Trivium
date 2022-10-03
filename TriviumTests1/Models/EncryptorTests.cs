using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trivium.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivium.Models.Tests
{
    [TestClass()]
    public class EncryptorTests
    {
        [TestMethod()]
        public void RunTest()
        {
            var key = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var sut = new Encryptor(key);
            var result = sut.Encrypt("abcba");
            Console.WriteLine(result);
        }
    }
}