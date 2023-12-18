using System.Collections.Generic;

namespace SODP.Domain.ValueObjects;

public record DesignerTitle	:ValueObject
{
	public string Value { get; }

	public DesignerTitle(string title)
	{
		Value = title ?? "";
	}

	public static DesignerTitle Default => new("");

	public static implicit operator string(DesignerTitle title) => title?.Value;

	public static implicit operator DesignerTitle(string title) => new(title);

	public override IEnumerable<object> GetAtomicValues()
	{
		yield return Value;
	}
}
