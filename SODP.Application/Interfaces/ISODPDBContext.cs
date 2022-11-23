using Microsoft.EntityFrameworkCore;
using SODP.Model;
using System.Threading.Tasks;
using System.Threading;

namespace SODP.Application.Interfaces
{
    public interface ISODPDBContext
    {
        public DbSet<Stage> Stages { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectBranch> ProjectBranches { get; set; }
        public DbSet<ProjectBranchRole> ProjectBranchRole { get; set; }
        public DbSet<Designer> Designers { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<LicenseBranch> BranchLicenses { get; set; }
        public DbSet<Certificate> Certificates { get; set; }
        public DbSet<User> Users { get; set; }

        Task<int> SaveChangesAsync();

    }
}
