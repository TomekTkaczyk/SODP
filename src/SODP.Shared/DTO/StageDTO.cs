namespace SODP.Shared.DTO;

public record StageDTO
{
	public int Id { get; set; } 

	public string Sign { get; set; }

	public string Title { get; set; }

	public bool ActiveStatus { get; set; }

	public override string ToString()
	{
		return $"({Sign.Trim()}) {Title.Trim()}";
	}
}
