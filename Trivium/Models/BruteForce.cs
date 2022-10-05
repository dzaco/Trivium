using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivium.Models
{
    public class BruteForce
    {
        private readonly string text;
        private readonly string encyptedText;
        private readonly Decryptor decryptor;
        private readonly int bitLength;

        public BruteForce(string text, string encyptedText, Decryptor decryptor)
        {
            this.text = text;
            this.encyptedText = encyptedText;
            this.decryptor = decryptor;
        }

        public IEnumerable<AtackResult> Atack()
        {
            foreach (var key in GetAllCombination())
            {
                var result = CreateResult();
                yield return result;
            }
        }

        private IEnumerable<BitArray> GetAllCombination()
        {
            throw new NotImplementedException();
        }

        private AtackResult CreateResult()
        {
            throw new NotImplementedException();
        }
    }
}