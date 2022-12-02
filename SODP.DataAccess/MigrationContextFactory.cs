using AutoMapper.Configuration.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Pomelo.EntityFrameworkCore.MySql.Storage;
using SODP.Application.Interfaces;
using SODP.Infrastructure.Services;
using System;
using System.Globalization;

namespace SODP.DataAccess
{
    public class MigrationContextFactory : IDesignTimeDbContextFactory<SODPDBContext>
    {
        public SODPDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SODPDBContext>();
            optionsBuilder.UseMySql("Server=localhost;Database=SODP;Uid=sodpdbuser;Pwd=sodpdbpassword;", 
                builder => builder.ServerVersion(new ServerVersion(new Version(10,4,6),ServerType.MariaDb)));

            return new SODPDBContext(optionsBuilder.Options, new DateTimeService());
        }
    }
}
