using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using SODP.DataAccess;
using SODP.Infrastructure.Services;

namespace SODP.Infrastructure
{
	public class MigrationContextFactory : IDesignTimeDbContextFactory<SODPDBContext>
	{
		public SODPDBContext CreateDbContext(string[] args)
		{
			var connectionString = "Server=localhost;Database=SODP;Uid=root;Pwd=gonia68;";
			var optionsBuilder = new DbContextOptionsBuilder<SODPDBContext>();
			optionsBuilder.UseMySql(connectionString,
				builder => builder.ServerVersion(ServerVersion.AutoDetect(connectionString)));

			return new SODPDBContext(optionsBuilder.Options, new DateTimeService());
		}
	}
}
