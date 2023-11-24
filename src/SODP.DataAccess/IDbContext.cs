using Microsoft.EntityFrameworkCore;
using SODP.Domain.Entities;

namespace SODP.DataAccess;

public interface IDbContext
{
	DbSet<AppDictionary> AppDictionary { get; set; }
	DbSet<Stage> Stages { get; set; }
	DbSet<Part> Parts { get; set; }
	DbSet<Branch> Branches { get; set; }
	DbSet<Project> Projects { get; set; }
	DbSet<ProjectPart> ProjectParts { get; set; }
	DbSet<PartBranch> PartBranches { get; set; }
	DbSet<BranchRole> BranchRoles { get; set; }
	DbSet<Designer> Designers { get; set; }
	DbSet<License> Licenses { get; set; }
	DbSet<BranchLicense> BranchLicenses { get; set; }
	DbSet<Certificate> Certificates { get; set; }
	DbSet<Investor> Investors { get; set; }
}
