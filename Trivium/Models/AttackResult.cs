using System;
using System.Numerics;

namespace Trivium.Models
{
    public class AttackResult
    {
        public DateTime TimeStamp { get; set; }
        public string Id { get; set; }
        public string DecryptedText { get; set; }

        internal bool IsMatch(string text)
        {
            return text == DecryptedText;
        }

        public AttackResult(string id, string decryptedText)
        {
            TimeStamp = DateTime.Now;
            Id = id;
            DecryptedText = decryptedText;
        }

        public override string ToString()
        {
            return $"[{TimeStamp}] ID=\'{Id}\' DecryptedText=\'{DecryptedText}\'";
        }
    }
}