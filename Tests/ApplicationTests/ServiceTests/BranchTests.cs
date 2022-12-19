using Microsoft.EntityFrameworkCore;
using SODP.DataAccess;
using SODP.Infrastructure.Services;
using SODP.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Tests.ApplicationTests.ServiceTests
{
    public class BranchTests
    {
        [Fact]
        public void SampleTest()
        {
            var context = new SODPDBContext(new DbContextOptions<SODPDBContext>(), new DateTimeService());
            var projects = context.Projects.ToList();

            Assert.NotNull(projects);
        }

    }
}
