namespace SODP.Shared.DTO;

public record PartDTO : NewPartDTO
{
	public int Order { get; set; }

	public bool ActiveStatus { get; set; }
}
