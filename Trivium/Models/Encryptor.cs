using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Trivium.Models
{
    public class Encryptor
    {
        private readonly int STATE_SIZE = 36;
        private Random random;
        private int[] key;
        private int[] iv;
        private int[] state;

        public Encryptor(int[] key)
        {
            this.random = new Random();
            this.state = new int[STATE_SIZE];
            this.key = key;
            this.iv = new int[10];
            Init();
        }

        private void Init()
        {
            FillIVWithRandomNumbers();

            insert_bits(state, 1, key, 80);
            insert_bits(state, 94, iv, 80);

            change_bit(state, 286, 1);
            change_bit(state, 287, 1);
            change_bit(state, 288, 1);
            initialize_state(state);
        }

        private void FillIVWithRandomNumbers()
        {
            for (var i = 0; i < 10; i++)
            {
                iv[i] = get_random_byte();
            }
        }

        public string Encrypt(string text)
        {
            var builder = new StringBuilder();
            foreach (var character in text)
            {
                var encChar = character ^ get_byte_from_gamma(state);
                builder.Append((char)encChar);
            }

            return builder.ToString();
        }

        private void change_bit(int[] arr, int bitIndex, int value)
        {
            var nbyte = (bitIndex - 1) / 8;
            var nbit = ((bitIndex - 1) % 8) + 1;

            arr[nbyte] =
                ((255 << (9 - nbit)) &
                arr[nbyte]) |
                (value << (8 - nbit)) |
                ((255 >> nbit) & arr[nbyte]);
        }

        private int nbit(int[] arr, int n)
        {
            int nbyte = (n - 1) / 8;
            int nbit = ((n - 1) % 8) + 1;
            return (arr[nbyte] & (1 << (8 - nbit))) >> (8 - nbit);
        }

        private int rotate(int[] arr, int arr_size)
        {
            int i;

            int a1 = nbit(arr, 91) & nbit(arr, 92);
            int a2 = nbit(arr, 175) & nbit(arr, 176);
            int a3 = nbit(arr, 286) & nbit(arr, 287);

            int t1 = nbit(arr, 66) ^ nbit(arr, 93);
            int t2 = nbit(arr, 162) ^ nbit(arr, 177);
            int t3 = nbit(arr, 243) ^ nbit(arr, 288);

            int result = t1 ^ t2 ^ t3;

            int s1 = a1 ^ nbit(arr, 171) ^ t1;
            int s2 = a2 ^ nbit(arr, 264) ^ t2;
            int s3 = a3 ^ nbit(arr, 69) ^ t3;

            /* Begin rotate */

            for (i = arr_size - 1; i > 0; i--)
            {
                arr[i] = (arr[i - 1] << 7) | (arr[i] >> 1);
            }
            arr[0] = arr[0] >> 1;

            /* End rotate */

            change_bit(arr, 1, s3);
            change_bit(arr, 94, s1);
            change_bit(arr, 178, s2);

            return result;
        }

        private void insert_bits(int[] arr, int startIndex, int[] source, int length)
        {
            int i;
            for (i = 0; i < length; i++)
            {
                change_bit(arr, startIndex + i, nbit(source, i + 1));
            }
        }

        private void initialize_state(int[] arr)
        {
            int i;
            for (i = 0; i < 4 * 288; i++)
            {
                rotate(arr, STATE_SIZE);
            }
        }

        private int get_byte_from_gamma(int[] arr)
        {
            int buf = 0;
            int i = 0;
            while (i != 8)
            {
                int z = rotate(arr, STATE_SIZE);
                buf = buf | (z << i);
                i += 1;
            }
            return buf;
        }

        private int get_random_byte()
        {
            return random.Next(255);
        }
    }
}