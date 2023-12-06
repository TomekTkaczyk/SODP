namespace SODP.Domain.ValueObjects;

public record DesignerTitle
{
	public string Value { get; }

	public DesignerTitle(string title)
	{
		Value = title;
	}

	public static implicit operator string(DesignerTitle title) => title?.Value;

	public static implicit operator DesignerTitle(string title) => new(title);
}
