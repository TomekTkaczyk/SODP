using SODP.Domain.ValueObjects;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace SODP.Domain.Entities;

public class ProjectPart : BaseEntity
{
    public int ProjectId { get; set; }
    public virtual Project Project { get; private set; }
	public Sign Sign { get; private set; }
	public Title Title { get; private set; }
    public int Order { get; private set; } = 1;
	public IReadOnlyCollection<PartBranch> Branches { get; private set; }

	private ProjectPart() { }

    private ProjectPart(Project project, Sign sign, Title title)
    {
        Project = project;
        Sign = sign;
        Title = title;
    }

	public static ProjectPart Create(Project project, Sign sign, Title title)
	{
		return new ProjectPart(project, sign, title);
	}

    public void Update(Sign sign, Title title)
    {
        this.Sign = sign;
        this.Title = title;
    }
}
