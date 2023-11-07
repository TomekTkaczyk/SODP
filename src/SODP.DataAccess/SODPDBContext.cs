using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SODP.Domain.Entities;
using SODP.Domain.Services;
using System.IO;
using System.Threading.Tasks;

namespace SODP.DataAccess;

public class SODPDBContext : IdentityDbContext<User, Role, int>
{
	private readonly IDateTime _dateTime;

	public SODPDBContext(DbContextOptions<SODPDBContext> options) : base(options) { }

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
					entry.Entity.SetCreateTimeStamp(_dateTime.Now);
					break;

				case EntityState.Modified:
					entry.Entity.SetModifyTimeStamp(_dateTime.Now);
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
		//optionsBuilder.EnableSensitiveDataLogging(true);
		//if (!optionsBuilder.IsConfigured)
		//{
		//	// Tutaj dodaj konfigurację połączenia do bazy danych, na przykład z pliku appsettings.json
		//	IConfigurationRoot configuration = new ConfigurationBuilder()
		//		.SetBasePath(Directory.GetCurrentDirectory())
		//		.AddJsonFile("appsettings.json")
		//		.Build();

		//	optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
		//}

	}
}
