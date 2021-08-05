using AutoMapper;
using SODP.Domain;
using Xunit;

namespace Tests.MappTests
{
    public class MappTests
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            config.AssertConfigurationIsValid();
        }
    }
}
