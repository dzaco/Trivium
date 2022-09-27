using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trivium.Models
{
    public class CryptoKey : IEquatable<CryptoKey>
    {
        public string Value { get; set; } = string.Empty;
        public int Length => Value.Length;

        public bool Equals(CryptoKey? other)
        {
            if (other is null)
                return false;
            return this.Value.Equals(other);
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
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value;
        }
    }
}