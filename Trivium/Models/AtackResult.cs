using System;
using System.Numerics;

namespace Trivium.Models
{
    public class AtackResult
    {
        public BigInteger Id { get; set; }
        public string EncryptedText { get; set; }
        public DateTime Duration { get; set; }
    }
}