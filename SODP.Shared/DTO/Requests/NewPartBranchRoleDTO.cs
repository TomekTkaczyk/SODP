using SODP.Shared.Enums;

namespace SODP.Shared.DTO.Requests
{
	public class NewPartBranchRoleDTO
	{
		public int partBranchId { get; set; }

		public TechnicalRole Role { get; set; }

		public int LicenseId { get; set; }
	}
}
