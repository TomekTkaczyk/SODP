using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SODP.Model;
using SODP.Shared.Interfaces;
using System.Threading.Tasks;

namespace SODP.DataAccess
{
    public class SODPDBContext : IdentityDbContext<User, Role, int>
    {
        private readonly IDateTime _dateTime;
        //private readonly IWebHostEnvironment _env;

        public SODPDBContext(DbContextOptions<SODPDBContext> options, IDateTime dateTime) : base(options)
        {
            _dateTime = dateTime;
        }

        public virtual DbSet<AppDictionary> AppDictionary { get; set; }
        public virtual DbSet<Stage> Stages { get; set; }
        public virtual DbSet<Part> Parts { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<ProjectPart> ProjectParts { get; set; }
        public virtual DbSet<PartBranch> PartBranches { get; set; }
        public virtual DbSet<BranchRole> BranchRoles { get; set; }
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

            modelBuilder.HasDefaultSchema(null);
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging(true);

        }
    }
}
