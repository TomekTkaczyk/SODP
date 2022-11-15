using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SODP.DataAccess.Configurations;
using SODP.Model;

namespace SODP.DataAccess
{
    public class SODPDBContext : IdentityDbContext<User, Role, int>
    {
        public SODPDBContext(DbContextOptions<SODPDBContext> options) : base(options) { }

        public virtual DbSet<Stage> Stages { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectBranch> ProjectBranches { get; set; }
        public virtual DbSet<ProjectBranchRole> ProjectBranchRole { get; set; }
        public virtual DbSet<Designer> Designers { get; set; }
        public virtual DbSet<License> Licenses { get; set; }
        public virtual DbSet<LicenseBranch> BranchLicenses { get; set; }
        public virtual DbSet<Certificate> Certificates { get; set; }




        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new UserEntityConfiguration().Configure(modelBuilder.Entity<User>());
            new RoleEntityConfiguration().Configure(modelBuilder.Entity<Role>());

            new TokenEntityConfiguration().Configure(modelBuilder.Entity<Token>());

            new StageEntityConfiguration().Configure(modelBuilder.Entity<Stage>());
            new BranchEntityConfiguration().Configure(modelBuilder.Entity<Branch>());
            new ProjectEntityConfiguration().Configure(modelBuilder.Entity<Project>());
            new ProjectBranchEntityConfiguration().Configure(modelBuilder.Entity<ProjectBranch>());
            new ProjectBranchRoleEntityConfiguration().Configure(modelBuilder.Entity<ProjectBranchRole>());
            new DesignerEntityConfiguration().Configure(modelBuilder.Entity<Designer>());
            new LicenseEntityConfiguration().Configure(modelBuilder.Entity<License>());
            new LicenseBranchEntityConfiguration().Configure(modelBuilder.Entity<LicenseBranch>());
            new CertificateEntityConfiguration().Configure(modelBuilder.Entity<Certificate>());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}
