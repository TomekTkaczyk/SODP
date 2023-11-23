namespace SODP.Domain.ValueObjects;

public record Sign
	{
		public string Value { get; }

    public Sign(string sign)
    {                           
        Value = sign.ToUpper();
    }

	public static implicit operator string(Sign sign) => sign.Value;

	public static implicit operator Sign(string sign) => new(sign);
}
