using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using SODP.DataAccess;

namespace SODP.Infrastructure;

public class MigrationContextFactory : IDesignTimeDbContextFactory<SODPDBContext>
{
	public SODPDBContext CreateDbContext(string[] args)
	{
		var connectionString = "Server=localhost;Database=SODP;Uid=sodpdbuser;Pwd=sodpdbpassword;";
		var optionsBuilder = new DbContextOptionsBuilder<SODPDBContext>();
		optionsBuilder.UseMySql(connectionString,
			builder => builder.ServerVersion(ServerVersion.AutoDetect(connectionString)));

		return new SODPDBContext(optionsBuilder.Options);
	}
}
