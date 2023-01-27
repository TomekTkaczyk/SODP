using System.Collections.Generic;

namespace SODP.Model;

public class ProjectPart : BaseEntity
{
    public int ProjectId { get; set; }
    public virtual Project Project { get; set; }
	public int PartId { get; set; }
	public virtual Part Part { get; set; }
	public ICollection<PartBranch> Branches { get; set; }
}
