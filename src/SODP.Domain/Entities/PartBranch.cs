using System.Collections.Generic;

namespace SODP.Domain.Entities;

public class PartBranch : BaseEntity
{
    private PartBranch() { }

    private PartBranch(ProjectPart part, Branch branch)
    {
        ProjectPartId = part.Id;
        ProjectPart = part;
        BranchId = branch.Id;
        Branch = branch;
    }

    public int ProjectPartId { get; set; }

    public virtual ProjectPart ProjectPart { get; set; }

    public int BranchId { get; set; }

    public virtual Branch Branch { get; set; }

    public ICollection<BranchRole> Roles { get; private set; } = new List<BranchRole>();

	public static PartBranch Create(ProjectPart part, Branch branch)
	{
        return new PartBranch(part, branch);
	}
}
