using System.Collections.Generic;
using SODP.Domain.Abstractions;

namespace SODP.Domain.ValueObjects
{
    public sealed record Error : ValueObject
    {
        public static readonly Error None = new(string.Empty, string.Empty);
        public static readonly Error NullValue = new("Error.NullValue", "The specified result value is null.");

        public string Code { get; }

        public string Message { get; }

        public Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Code;
            yield return Message;
        }

        public static implicit operator string(Error error) => error?.Code ?? string.Empty;
    }
}