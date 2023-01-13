using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using SODP.Shared.Services;
using System;
using System.IO;

namespace SODP.DataAccess
{
    public class MigrationContextFactory : IDesignTimeDbContextFactory<SODPDBContext>
    {
        public SODPDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SODPDBContext>();
            optionsBuilder.UseMySql("Server=localhost;Database=SODP;Uid=root;Pwd=gonia68;", 
                builder => builder.ServerVersion(new ServerVersion(new Version(10,4,6),ServerType.MariaDb)));

            return new SODPDBContext(optionsBuilder.Options, new DateTimeService());
        }
    }
}
