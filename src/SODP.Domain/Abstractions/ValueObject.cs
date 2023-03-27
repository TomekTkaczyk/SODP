using System;
using System.Collections.Generic;

namespace SODP.Domain.Abstractions
{
    public abstract record ValueObject : IEquatable<ValueObject>
    {
        public override int GetHashCode()
        {
            HashCode hashCode = default;

            foreach (object obj in GetAtomicValues())
            {
                hashCode.Add(obj);
            }

            return hashCode.ToHashCode();
        }

        protected abstract IEnumerable<object> GetAtomicValues();
    }
}
