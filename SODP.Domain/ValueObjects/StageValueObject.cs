using SODP.Model;
using System;

namespace SODP.Domain.ValueObjects
{
    public record StageValueObject 
    {
        public Stage Value { get; }

        public StageValueObject(string sign, string name)
        {
            if (string.IsNullOrWhiteSpace(sign))
            {
                throw new ArgumentException("Sign is empty.");
            }

            Value = new Stage(sign,name);
        }
    }
}
