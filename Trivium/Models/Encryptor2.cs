using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivium.Models
{
    public class Encryptor2
    {
        private readonly Int16 stateSize = 288;
        private readonly Random random;
        private readonly CryptoKey cryptoKey;
        private readonly int length;
        private BitArray state;
        private BitArray key;
        private BitArray iv;

        public Encryptor2(CryptoKey cryptoKey)
        {
            this.random = new Random();
            this.cryptoKey = cryptoKey;
            this.length = cryptoKey.Length;
            this.state = new BitArray(stateSize);
            this.key = new BitArray(cryptoKey.Bytes);
            this.iv = CreateBitArrayWithRandomBits();
            this.Init();
        }

        private BitArray CreateBitArrayWithRandomBits()
        {
            var bytes = new byte[length];
            for (var i = 0; i < length; i++)
            {
                bytes[i] = (byte)random.Next(255);
            }
            return new BitArray(bytes);
        }

        private void Init()
        {
            state.Insert(key, 0, 80);
            state.Insert(iv, 93, 80);

            state.Set(285, true);
            state.Set(286, true);
            state.Set(287, true);

            for (int i = 0; i < 4 * stateSize; i++)
            {
                Rotate(state);
            }
        }

        private bool Rotate(BitArray arr)
        {
            var t1 = arr.Get(65) ^ arr.Get(92);
            var t2 = arr.Get(161) ^ arr.Get(176);
            var t3 = arr.Get(242) ^ arr.Get(287);

            var result = t1 ^ t2 ^ t3;

            var a1 = arr.Get(90) & arr.Get(91);
            var a2 = arr.Get(174) & arr.Get(175);
            var a3 = arr.Get(285) & arr.Get(286);

            t1 = t1 ^ a1 ^ arr.Get(170);
            t2 = t2 ^ a2 ^ arr.Get(263);
            t3 = t3 ^ a3 ^ arr.Get(68);

            var s1_92 = arr.Get(from: 0, length: 92);
            var s94_176 = arr.Get(from: 93, length: 83);
            var s178_287 = arr.Get(from: 177, length: 110);

            //{s1, s2, ... s92, s93} <= {T3, s1, s2, ..., s91, s92}
            arr.Set(0, t3);
            arr.Insert(s1_92, startPosition: 1, s1_92.Length);

            //{s94, s95, ... s176, s177} <= {T1, s94, s95, ..., s175, s176}
            arr.Set(93, t1);
            arr.Insert(s94_176, startPosition: 94, s94_176.Length);

            //{s178, s179, ... s287, s288} <= {T2, s178, s179, ..., s286, s287}
            arr.Set(177, t2);
            arr.Insert(s178_287, startPosition: 178, s178_287.Length);

            return result;
        }

        public string Encrypt(string text)
        {
            var builder = new StringBuilder();
            foreach (var character in text)
            {
                var encChar = character ^ GetGammaByte();
                builder.Append((char)encChar);
            }

            return builder.ToString();
        }

        private int GetGammaByte()
        {
            int buffer = 0;
            var i = 0;
            while (i != 8)
            {
                var res = Rotate(state) ? 1 : 0;
                buffer = buffer | (res << i);
                i++;
            }
            return buffer;
        }
    }
}