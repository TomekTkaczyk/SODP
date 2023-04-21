using System;

namespace SODP.Shared.ValueObjects;

public record Differentiator
{
    public string Sign { get; set; }
    
    public string Title { get; set; }

    public Differentiator(string sign): this(sign, string.Empty) { }

    public Differentiator(string sign, string title)
    {
		if (sign is null || string.IsNullOrWhiteSpace(sign))
		{
			throw new ArgumentException("Bad sign value.");
		}

		Sign = sign.ToUpper();
        Title = title.ToUpper();
    }

    public override string ToString()
    {
        return $"{Sign} {Title}";
    }
}
