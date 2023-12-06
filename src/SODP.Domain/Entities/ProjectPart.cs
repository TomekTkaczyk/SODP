using SODP.Domain.Exceptions.PartExceptions;
using SODP.Domain.ValueObjects;
using System.Collections.Generic;

namespace SODP.Domain.Entities;

public class ProjectPart : BaseEntity
{
    public int ProjectId { get; set; }
    public virtual Project Project { get; set; }
	public Sign Sign { get; set; }
	public Title Title { get; set; }
    public int Order { get; private set; } = 1;
	public ICollection<PartBranch> Branches { get; set; }

	private ProjectPart() { }

	private ProjectPart(Project project, Part part)
	{
		Project = project;
		Sign = part.Sign;
		Title = part.Title;
	}

	public static ProjectPart Create(Project project, Part part)
	{
		if (part is null)
		{
			throw new PartIsNullException();
		}

		return new ProjectPart(project, part);
	}


}
