using System;
using System.Numerics;

namespace Trivium.Models
{
    public class AttackResult
    {
        public string Id { get; set; }
        public string DecryptedText { get; set; }

        internal bool IsMatch(string text)
        {
            return text == DecryptedText;
        }

        public override string ToString()
        {
            return $"[{DateTime.Now}] ID=\'{Id}\' DecryptedText=\'{DecryptedText}\'";
        }
    }
}