using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivium.Models
{
    public class CryptoKey : IEquatable<CryptoKey>
    {
        private string _stringValue;

        public string Value
        {
            get { return _stringValue; }
            set
            {
                _stringValue = value;
                Bytes = ConvertToBytes(value);
            }
        }

        public int Length { get; set; }
        public byte[] Bytes { get; private set; }
        public bool IsValid { get; private set; }

        public CryptoKey()
        {
            Value = String.Empty;
            Length = (int)BinaryLength.bit64;
            Bytes = new byte[0];
        }

        private byte[] ConvertToBytes(string value)
        {
            try
            {
                if (value.Length % 2 != 0)
                    throw new FormatException("Hex must has event length");

                var bytes = Convert.FromHexString(value);
                return bytes;
            }
            catch (Exception)
            {
                this.IsValid = false;
                return Bytes;
            }
        }

        public bool Equals(CryptoKey? other)
        {
            if (other is null)
                return false;
            return this.Value.Equals(other.Value) &&
                this.Length.Equals(other.Length);
        }

        public override bool Equals(object? obj)
        {
            switch (obj)
            {
                case null:
                    return false;

                case CryptoKey:
                    return this.Equals((CryptoKey)obj);

                case string:
                    return this.Value.Equals((string)obj);

                default:
                    return false;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.Value, this.Length);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}