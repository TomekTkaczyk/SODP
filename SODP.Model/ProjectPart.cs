using System.Collections.Generic;

namespace SODP.Model;

public class ProjectPart : BaseEntity
{
    public int ProjectId { get; set; }
    public virtual Project Project { get; set; }
		public string Sign { get; set; }
		public string Name { get; set; }
		public ICollection<PartBranch> Branches { get; set; }
}
