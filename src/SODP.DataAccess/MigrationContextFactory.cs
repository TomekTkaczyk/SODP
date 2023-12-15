using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using SODP.DataAccess;

namespace SODP.Infrastructure;

public class MigrationContextFactory : IDesignTimeDbContextFactory<SODPDBContext>
{
	public SODPDBContext CreateDbContext(string[] args)
	{
		var connectionString = "Server=localhost;Database=SODP;Uid=sodpdbuser;Pwd=sodpdbpassword;";
		var optionsBuilder = new DbContextOptionsBuilder<SODPDBContext>()
			.UseMySql(connectionString, new MariaDbServerVersion(ServerVersion.AutoDetect(connectionString)),
				b =>
				{
					b.SchemaBehavior(MySqlSchemaBehavior.Ignore);
				})
			.Options;

		return new SODPDBContext(optionsBuilder);
	}
}
