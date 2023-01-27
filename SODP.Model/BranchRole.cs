using SODP.Shared.Enums;

namespace SODP.Model;

public class BranchRole : BaseEntity
{
    public int PartBranchId { get; set; }

    public virtual PartBranch PartBranch { get; set; }

    public TechnicalRole Role { get; set; }

    public int LicenseId { get; set; }

    public virtual License License { get; set; }

}
