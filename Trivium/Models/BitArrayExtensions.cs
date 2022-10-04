using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace Trivium.Models
{
    public static class BitArrayExtensions
    {
        public static void Insert(this BitArray bitArray, BitArray source, int startPosition, int length)
        {
            for (int i = 0; i < length; i++)
            {
                bitArray.Set(startPosition + i, source.Get(i));
            }
        }

        public static BitArray Get(this BitArray bitArray, int from, int length)
        {
            var bits = new bool[length];
            for (int i = 0; i < length; i++)
            {
                bits[i] = bitArray.Get(from + i);
            }
            return new BitArray(bits);
        }
    }
}