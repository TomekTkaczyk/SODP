using SODP.Domain.Exceptions.BranchExceptions;
using SODP.Domain.Exceptions.LicenseExceptions;
using System.Collections.Generic;
using System.Linq;

namespace SODP.Domain.Entities;

public class License : BaseEntity
{
    public int DesignerId { get; set; }
    public virtual Designer Designer { get; set; }
    public string Content { get; set; }
    public virtual ICollection<BranchLicense> Branches { get; set; }

    public static License Create(Designer designer, string content)
    {
        return new License()
        {
            Designer = designer,
            Content = content.ToUpper()
        };
    }

    public void AddBranch(Branch branch)
    {
        if( Branches.FirstOrDefault(x => x.BranchId == branch.Id) != null )
        {
            throw new LicenceBranchConflictException();
		}

        Branches.Add(new BranchLicense()
        {
            BranchId = branch.Id,
            LicenseId = Id
        });
    }

    public bool DeleteBranch(int branchId)
    {
        var toDelete = Branches.Where(x => x.BranchId == branchId).SingleOrDefault()
            ?? throw new BranchNotFoundException();
        
        return Branches.Remove(toDelete);
    }
}
