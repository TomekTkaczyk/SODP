using System.Collections.Generic;

namespace SODP.Domain.ValueObjects;

public record Sign : ValueObject
	{
		public string Value { get; }

    public Sign(string sign)
    {                           
        Value = sign.ToUpper() ?? "";
    }

	public static Sign Default => new("");

	public static implicit operator string(Sign sign) => sign.Value;

	public static implicit operator Sign(string sign) => new(sign);

	public override IEnumerable<object> GetAtomicValues()
	{
		yield return Value;
	}
}
