using AutoFixture;
using AutoMapper;
using SODP.Domain;
using SODP.Domain.Entities;
using SODP.Shared.DTO;
using System.Linq;
using Xunit;

namespace tests.MappTests
{
	public class MappLicenseTests
    {
        [Fact]
        internal void AutoMapper_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            config.AssertConfigurationIsValid();
        }

        [Fact]
        internal void AutoMapper_ConvertFromLicense_IsValid()
        {
            var fixture = new Fixture();
            fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            LicenseDTO dto = fixture.Create<LicenseDTO>();
            License entity = fixture.Create<License>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            var mapper = config.CreateMapper();
            var licenseDTO = mapper.Map<License, LicenseDTO>(entity);

            Assert.NotNull(licenseDTO.Designer);
        }

        [Fact]
        internal void AutoMapper_ConvertFromLicense_ToLicenseWithBranch_IsValid()
        {
            var fixture = new Fixture();
            fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            LicenseDTO dto = fixture.Create<LicenseDTO>();
            License entity = fixture.Create<License>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            var mapper = config.CreateMapper();
            var licenseDTO = mapper.Map<License, LicenseDTO>(entity);

            Assert.NotNull(licenseDTO.Designer);
        }
    }
}
