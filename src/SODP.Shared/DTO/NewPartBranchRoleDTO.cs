using SODP.Shared.Enums;

namespace SODP.Shared.DTO
{
    public class NewPartBranchRoleDTO
    {
        public int partBranchId { get; set; }

        public TechnicalRole Role { get; set; }

        public int LicenseId { get; set; }
    }
}
