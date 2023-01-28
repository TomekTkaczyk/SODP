using System.Collections.Generic;

namespace SODP.Model;

public class PartBranch : BaseEntity
{
    public int ProjectPartId { get; set; }

    public virtual ProjectPart ProjectPart { get; set; }

    public ICollection<BranchRole> Roles { get; set; }

}
