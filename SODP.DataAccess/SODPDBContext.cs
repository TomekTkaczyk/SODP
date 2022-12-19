using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SODP.DataAccess.Configurations;
using SODP.Model;
using SODP.Shared.Interfaces;
using System.Threading.Tasks;

namespace SODP.DataAccess
{
    public class SODPDBContext : IdentityDbContext<User, Role, int>
    {
        private readonly IDateTime _dateTime;

        public SODPDBContext(DbContextOptions<SODPDBContext> options, IDateTime dateTime) : base(options)
        {
            _dateTime = dateTime;
        }

        public virtual DbSet<Stage> Stages { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectBranch> ProjectBranches { get; set; }
        public virtual DbSet<ProjectBranchRole> ProjectBranchRole { get; set; }
        public virtual DbSet<Designer> Designers { get; set; }
        public virtual DbSet<License> Licenses { get; set; }
        public virtual DbSet<LicenseBranch> BranchLicenses { get; set; }
        public virtual DbSet<Certificate> Certificates { get; set; }
        public virtual DbSet<Investor> Investors { get; set; }

        public async Task<int> SaveChangesAsync()
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<BaseEntity> entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreateTimeStamp = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifyTimeStamp = _dateTime.Now;
                        break;
                }
            }
            var result = await base.SaveChangesAsync();

            return result;
        }


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
            new InvestorEntityConfiguration().Configure(modelBuilder.Entity<Investor>());
      
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(false);
        }
    }
}
