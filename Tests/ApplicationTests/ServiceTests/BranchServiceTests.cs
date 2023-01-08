using SODP.Application.Validators;
using SODP.Model;
using System;

namespace Tests.ApplicationTests.ServiceTests
{
    public class BranchServiceTests : ServiceTest<Branch>, IDisposable
    {
        public BranchServiceTests() : base(new BranchValidator()) { }

    }
}
