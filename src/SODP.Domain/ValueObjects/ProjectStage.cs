using SODP.Domain.Exceptions.ValueObjectExceptions;

namespace SODP.Domain.ValueObjects;

public record ProjectStage(string Sign, string Title)
{
	public string Sign { get; } = Sign ?? throw new EmptyStageSignException();

	public override string ToString()
	{
		return $"{Sign.Trim()} {Title.Trim()}";
	}
}
