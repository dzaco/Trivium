using NUnit.Framework;

namespace Trivium.Models.Tests
{
    public class EncryptorTests
    {
        [Test]
        public void InitTest()
        {
            var encryptor = new Encryptor();
        }

        [Test]
        public void RunTest()
        {
            Console.WriteLine("Test");
        }
    }
}