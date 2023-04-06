namespace SODP.Shared.DTO;

public record NewBranchDTO // : BaseDTO
{
	public int Id { get; set; }

	public string Sign { get; set; }

	public string Name { get; set; }

	public override string ToString()
	{
		return $"{Sign.Trim()} {Name.Trim()}";
	}
}
