using SODP.Shared.Enums;

namespace SODP.Domain.Entities;

public class BranchRole : BaseEntity
{
    public int PartBranchId { get; set; }

    public virtual PartBranch PartBranch { get; set; }

    public TechnicalRole Role { get; set; }

    public int LicenseId { get; set; }

    public virtual License License { get; set; }

    private BranchRole() { }

    private BranchRole(TechnicalRole role, License license)
    {
        Role = role;
        License = license;
    }

    public static BranchRole Create(TechnicalRole role, License license)
    {
       return new BranchRole(role, license);
    }
}
