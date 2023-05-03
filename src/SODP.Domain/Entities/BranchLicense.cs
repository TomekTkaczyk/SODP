namespace SODP.Domain.Entities;

public class BranchLicense : BaseEntity
{
    public int BranchId { get; set; }
    public Branch Branch { get; set; }
    public int LicenseId { get; set; }
    public License License { get; set; }
}
