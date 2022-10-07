using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Trivium.Models
{
    public class BruteForce
    {
        private readonly string text;
        private readonly string encyptedText;
        private readonly Decryptor decryptor;
        private readonly int bitLength;
        public DateTime StartAttackTime;
        public DateTime EndAttackTime;

        public BruteForce(string text, string encyptedText, Decryptor decryptor)
        {
            this.text = text;
            this.encyptedText = encyptedText;
            this.decryptor = decryptor;
            this.bitLength = decryptor.BitLength;
            StartAttackTime = DateTime.MinValue;
            EndAttackTime = DateTime.MinValue;
        }

        public IEnumerable<AttackResult> Atack()
        {
            StartAttackTime = DateTime.Now;
            var currentTry = new BigInteger(0);
            var maxTry = 100;
            foreach (var key in GetAllCombination())
            {
                currentTry++;
                var result = TryDecrypt(key, currentTry);
                yield return result;
                if (result.IsMatch(text) || currentTry > maxTry)
                    break;
            }
            EndAttackTime = DateTime.Now;
        }

        public IEnumerable<BitArray> GetAllCombination()
        {
            var max = BigInteger.Pow(2, bitLength);
            var bigNumber = new BigInteger(0);
            var currentKey = new BitArray(bitLength);
            while (bigNumber <= max)
            {
                bigNumber++;
                currentKey.Set(new BitArray(bigNumber.ToByteArray()));
                yield return currentKey;
            }
        }

        private AttackResult TryDecrypt(BitArray key, BigInteger currentTry)
        {
            var decryptedText = this.decryptor.Encryptor.Encrypt(encyptedText);
            return new AttackResult(currentTry.ToString(), decryptedText);
        }

        public bool IsRunning()
        {
            return StartAttackTime != DateTime.MinValue &&
                EndAttackTime == DateTime.MinValue;
        }
    }
}