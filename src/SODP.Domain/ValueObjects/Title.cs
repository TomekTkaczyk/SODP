using System.Collections.Generic;

namespace SODP.Domain.ValueObjects;

public record Title : ValueObject
{
	public string Value { get; }

    public Title(string title)
    {
        Value = title ?? "";
    }

    public static Title Default => new("");

	public static implicit operator string(Title title) => title?.Value;

	public static implicit operator Title(string title) => new(title);

	public override IEnumerable<object> GetAtomicValues()
	{
		yield return Value;
	}
}
