using SODP.Domain.Exceptions.ValueObjectExceptions;
using System.Collections.Generic;

namespace SODP.Domain.ValueObjects;

public record ProjectStage(string Sign, string Title) : ValueObject
{
	public string Sign { get; } = Sign ?? throw new EmptyStageSignException();

	public override string ToString()
	{
		return $"{Sign.Trim()} {Title.Trim()}";
	}

	public override IEnumerable<object> GetAtomicValues()
	{
		yield return Sign;
		yield return Title;
	}
}
