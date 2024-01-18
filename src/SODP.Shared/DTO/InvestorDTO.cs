namespace SODP.Shared.DTO;

public record InvestorDTO
{
	public int Id { get; set; }
	public string Name { get; set; }
	public bool ActiveStatus { get; set; }
}
