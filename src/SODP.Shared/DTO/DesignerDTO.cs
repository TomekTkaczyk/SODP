namespace SODP.Shared.DTO;

public record DesignerDTO // : BaseDTO
{
	public int Id { get; set; }
	public string Title { get; set; } = string.Empty;
	public string Firstname { get; set; }
	public string Lastname { get; set; }
	public bool ActiveStatus { get; set; }
	public override string ToString()
	{
		return $"{(Title ?? string.Empty).Trim()} {(Firstname ?? string.Empty).Trim()} {(Lastname ?? string.Empty).Trim()}";
	}
}
