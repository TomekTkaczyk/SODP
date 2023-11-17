using SODP.Domain.Exceptions.PartExceptions;
using System.Collections.Generic;

namespace SODP.Domain.Entities;

public class ProjectPart : BaseEntity
{
    private ProjectPart() { }
    
    private ProjectPart(Project project, Part part)
    {                                            
        Project = project;
        Sign = part.Sign;
        Name = part.Title;
    }

    public static ProjectPart Create(Project project, Part part)
    {
        if(part is null)
        {
            throw new PartIsNullException();
        }

        return new ProjectPart(project, part);
    }

    public int ProjectId { get; set; }
    public virtual Project Project { get; set; }
	public string Sign { get; set; }
	public string Name { get; set; }
    public int Order { get; private set; } = 1;
	public ICollection<PartBranch> Branches { get; set; }
}
