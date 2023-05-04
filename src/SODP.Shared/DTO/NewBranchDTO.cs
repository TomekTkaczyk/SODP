namespace SODP.Shared.DTO;

public record NewBranchDTO
{
	public int Id { get; set; }

	public string Sign { get; set; }
	
	public string Title { get; set; }

	public override string ToString()
	{
		return $"{Sign} {Title}";
	}
}
