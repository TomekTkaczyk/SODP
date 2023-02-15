using AutoMapper;
using Xunit;

namespace Tests.MappTests
{
    public class MappTests
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => 
            { 
                cfg.AddProfile<SODP.Domain.AutoMapperProfile>(); 
                cfg.AddProfile<SODP.UI.Mappers.AutoMapperProfile>(); 
            });
            
            config.AssertConfigurationIsValid();
        }
    }
}
