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

        public static BitArray Reverse(this BitArray bitArray)
        {
            var result = new BitArray(bitArray.Length);
            for (var i = 0; i < bitArray.Length; i++)
            {
                var reverseIndex = bitArray.Length - 1 - i;
                result[reverseIndex] = bitArray[i];
            }
            return result;
        }

        public static void Set(this BitArray bitArray, BitArray source)
        {
            if (source.Length >= bitArray.Length)
                bitArray = new BitArray(source);

            var i = 0;
            for (; i < source.Length; i++)
            {
                bitArray.Set(i, source.Get(i));
            }
            for (; i < bitArray.Length; i++)
            {
                bitArray.Set(i, false);
            }
        }
    }
}