namespace SODP.Shared.DTO;

public class BranchRoleDTO : BaseDTO
{
	public string Role { get; set; }

	public virtual LicenseDTO License { get; set; }
}
