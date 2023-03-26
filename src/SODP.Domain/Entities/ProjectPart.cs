using System.Collections.Generic;

namespace SODP.Domain.Entities;

public class ProjectPart : BaseEntity
{
    private ProjectPart() { }
    
    public ProjectPart(Project project, Part part)
    {                                            
        Project = project;
        Sign = part.Sign;
        Name = part.Name;
    }
    public int ProjectId { get; set; }
    public virtual Project Project { get; set; }
	public string Sign { get; set; }
	public string Name { get; set; }
    public int Order { get; private set; } = 1;
	public ICollection<PartBranch> Branches { get; set; }
}
