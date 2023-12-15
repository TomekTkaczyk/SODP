using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using SODP.DataAccess;
using SODP.Infrastructure.Services;

namespace tests.Utils;

public class SODPDbContextFactory
{
    public SODPDbContextFactory() { }

    public static SODPDBContext CreateDbContext(string connectionString)
	{
        var options = new DbContextOptionsBuilder<SODPDBContext>()
			.UseMySql(
				connectionString,
				new MariaDbServerVersion(ServerVersion.AutoDetect(connectionString)),
				b =>
				{
					b.SchemaBehavior(MySqlSchemaBehavior.Ignore);
				})
			.Options;

        return new SODPDBContext(options, new DateTimeService());
    }
}