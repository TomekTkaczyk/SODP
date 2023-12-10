namespace SODP.Shared.DTO;

public record PartDTO
{
    public int Id { get; set; }

    public string Sign { get; set; }

    public string Title { get; set; }

    public int Order { get; set; }

	public bool ActiveStatus { get; set; }
}
