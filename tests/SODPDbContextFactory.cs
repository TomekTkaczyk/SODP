using Microsoft.EntityFrameworkCore;
using SODP.DataAccess;
using SODP.Infrastructure.Services;

namespace Tests;

public class SODPDbContextFactory
{
    public SODPDbContextFactory() { }

    public static SODPDBContext CreateDbContext(string connectionString)
    {
		var options = new DbContextOptionsBuilder<SODPDBContext>().UseMySql(connectionString).Options;
		return new SODPDBContext(options, new DateTimeService());
    }
}