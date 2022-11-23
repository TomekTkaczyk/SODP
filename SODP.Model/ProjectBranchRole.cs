using SODP.Shared.Enums;

namespace SODP.Model
{
    public class ProjectBranchRole
    {
        public int Id { get; set; }  // to do remove !!!

        public int ProjectBranchId { get; set; }

        public virtual ProjectBranch ProjectBranch { get; set; }

        public TechnicalRole Role { get; set; }

        public int LicenseId { get; set; }

        public virtual License License { get; set; }

    }
}
