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

        public virtual DbSet<AppDictionary> AppDictionary { get; set; }
        public virtual DbSet<Stage> Stages { get; set; }
        public virtual DbSet<Part> Parts { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectPart> ProjectPart { get; set; }
        public virtual DbSet<PartBranch> PartBranch { get; set; }
        public virtual DbSet<BranchRole> BranchRole { get; set; }
        public virtual DbSet<Designer> Designers { get; set; }
        public virtual DbSet<License> Licenses { get; set; }
        public virtual DbSet<BranchLicense> BranchLicenses { get; set; }
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
            new TokenEntityConfiguration().Configure(modelBuilder.Entity<Token>());
            new DesignerEntityConfiguration().Configure(modelBuilder.Entity<Designer>());
            new InvestorEntityConfiguration().Configure(modelBuilder.Entity<Investor>());
            new StageEntityConfiguration().Configure(modelBuilder.Entity<Stage>());
            new PartEntityConfiguration().Configure(modelBuilder.Entity<Part>());
            new BranchEntityConfiguration().Configure(modelBuilder.Entity<Branch>());
            new RoleEntityConfiguration().Configure(modelBuilder.Entity<Role>());
            new LicenseEntityConfiguration().Configure(modelBuilder.Entity<License>());
            new CertificateEntityConfiguration().Configure(modelBuilder.Entity<Certificate>());
            new ProjectEntityConfiguration().Configure(modelBuilder.Entity<Project>());
            new DictionaryEntityConfiguration().Configure(modelBuilder.Entity<AppDictionary>());

            new ProjectPartEntityConfiguration().Configure(modelBuilder.Entity<ProjectPart>());
            new PartBranchEntityConfiguration().Configure(modelBuilder.Entity<PartBranch>());
            new BranchRoleEntityConfiguration().Configure(modelBuilder.Entity<BranchRole>());
            new BranchLicenseEntityConfiguration().Configure(modelBuilder.Entity<BranchLicense>());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(true);
        }
    }
}
