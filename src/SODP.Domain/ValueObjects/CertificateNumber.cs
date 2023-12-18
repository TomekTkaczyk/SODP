using System.Collections.Generic;

namespace SODP.Domain.ValueObjects;

public record CertificateNumber	: ValueObject
{
	public string Value { get; }

	public CertificateNumber(string number)
	{
		Value = number ?? "";
	}

	public static CertificateNumber Default => new("");

	public static implicit operator string(CertificateNumber number) => number?.Value;

	public static implicit operator CertificateNumber(string number) => new(number);

	public override IEnumerable<object> GetAtomicValues()
	{
		yield return Value;
	}
}
