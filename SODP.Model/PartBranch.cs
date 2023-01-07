using System.Collections.Generic;

namespace SODP.Model;

public class PartBranch : BaseEntity
{
    public int ProjectPartId { get; set; }
    public virtual ProjectPart ProjectPart { get; set; }
    public int BranchId { get; set; }
    public virtual Branch Branch { get; set; }

    public ICollection<PartBranchRole> Roles { get; set; }

    public override string ToString()
    {
        return Branch.ToString();
    }
}
