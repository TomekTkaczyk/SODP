using SODP.Shared.ValueObjects;

namespace SODP.Shared.DTO;

public record NewPartDTO // : BaseDTO
{
	public int Id { get; set; }

	public string Sign { get; set; }

	public string Title { get; set; }

	public override string ToString()
	{
		return $"{Sign} {Title}";
	}
}