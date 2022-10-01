using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivium.Models
{
    public class CryptoKey : IEquatable<CryptoKey>
    {
        public string Value { get; set; }
        public int Length { get; set; }

        public CryptoKey()
        {
            Value = String.Empty;
            Length = (int)BinaryLength.bit64;
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