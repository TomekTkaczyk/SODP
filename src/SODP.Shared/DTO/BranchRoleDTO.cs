namespace SODP.Shared.DTO;

public record BranchRoleDTO
{
	public int Id { get; set; }

	public string Role { get; set; }

	public virtual LicenseDTO License { get; set; }
}
