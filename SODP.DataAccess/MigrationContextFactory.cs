using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using System;

namespace SODP.DataAccess
{
    public class MigrationContextFactory : IDesignTimeDbContextFactory<SODPDBContext>
    {
        public SODPDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SODPDBContext>();
            optionsBuilder.UseMySql("Server=192.168.1.16;Database=SODP;Uid=sodpdbuser;Pwd=sodpdbpassword;", 
                builder => builder.ServerVersion(new ServerVersion(new Version(10,4,6),ServerType.MariaDb)));

            return new SODPDBContext(optionsBuilder.Options);
        }
    }
}
