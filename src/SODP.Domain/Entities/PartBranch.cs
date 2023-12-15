using System.Collections.Generic;

namespace SODP.Domain.Entities;

public class PartBranch : BaseEntity
{
    private IList<BranchRole> _roles = new List<BranchRole>();

    public int ProjectPartId { get; set; }

    public virtual ProjectPart ProjectPart { get; set; }

    public int BranchId { get; set; }

    public virtual Branch Branch { get; set; }

    public ICollection<BranchRole> Roles { get; private set; }

}
