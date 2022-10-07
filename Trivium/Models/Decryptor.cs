using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivium.Models
{
    public class Decryptor
    {
        private readonly Int16 stateSize = 288;
        public readonly int BitLength;
        private readonly int byteLength;
        private BitArray state;
        private BitArray key;
        private BitArray iv;

        public Encryptor2 Encryptor { get; }

        public Decryptor(Encryptor2 encryptor)
        {
            this.state = new BitArray(stateSize);
            this.BitLength = encryptor.BitLength;
            this.byteLength = encryptor.ByteLength;
            this.key = encryptor.Key;
            this.iv = encryptor.IV;
            this.Init();
            Encryptor = encryptor;
        }

        private void Init()
        {
            state.Insert(key, 0, BitLength);
            state.Insert(iv, 93, BitLength);

            state.Set(285, true);
            state.Set(286, true);
            state.Set(287, true);

            for (int i = 0; i < 4 * stateSize; i++)
            {
                RotateState();
            }
        }

        private bool RotateState()
        {
            var t1 = state.Get(65) ^ state.Get(92);
            var t2 = state.Get(161) ^ state.Get(176);
            var t3 = state.Get(242) ^ state.Get(287);

            var result = t1 ^ t2 ^ t3;

            var a1 = state.Get(90) & state.Get(91);
            var a2 = state.Get(174) & state.Get(175);
            var a3 = state.Get(285) & state.Get(286);

            t1 = t1 ^ a1 ^ state.Get(170);
            t2 = t2 ^ a2 ^ state.Get(263);
            t3 = t3 ^ a3 ^ state.Get(68);

            var s1_92 = state.Get(from: 0, length: 92);
            var s94_176 = state.Get(from: 93, length: 83);
            var s178_287 = state.Get(from: 177, length: 110);

            //{s1, s2, ... s92, s93} <= {T3, s1, s2, ..., s91, s92}
            state.Set(0, t3);
            state.Insert(s1_92, startPosition: 1, s1_92.Length);

            //{s94, s95, ... s176, s177} <= {T1, s94, s95, ..., s175, s176}
            state.Set(93, t1);
            state.Insert(s94_176, startPosition: 94, s94_176.Length);

            //{s178, s179, ... s287, s288} <= {T2, s178, s179, ..., s286, s287}
            state.Set(177, t2);
            state.Insert(s178_287, startPosition: 178, s178_287.Length);

            return result;
        }

        public string Decrypt(string encryptedText)
        {
            var builder = new StringBuilder();
            foreach (var character in encryptedText)
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
                var res = RotateState() ? 1 : 0;
                buffer = buffer | (res << i);
                i++;
            }
            return buffer;
        }

        internal string Decrypt(string encyptedText, BitArray withKey)
        {
            this.key = withKey;
            return Decrypt(encyptedText);
        }
    }
}