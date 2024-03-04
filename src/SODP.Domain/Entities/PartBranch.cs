using SODP.Domain.Exceptions.ProjectExceptions;
using SODP.Shared.Enums;
using System.Collections.Generic;
using System.Linq;

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

    public void AddRole(TechnicalRole Role, License license)
    {
        if( Roles.Where(x => x.Role == Role).Any())
        {
            throw new BranchRoleExistException();
        }

        if(Roles.Where(x => x.LicenseId == license.Id).Any())
        {
            throw new LicenceAlreadyUsedException();
        }

        Roles.Add(BranchRole.Create(Role, license));
    }

    public bool RemoveRole(TechnicalRole role) 
    {
        return Roles.Remove(Roles.FirstOrDefault(x => x.Role == role));
    }
}
