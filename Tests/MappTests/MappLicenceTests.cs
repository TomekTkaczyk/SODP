using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoFixture;
using AutoFixture.Xunit2;
using AutoMapper;
using SODP.Domain;
using SODP.Model;
using SODP.Shared.DTO;
using Xunit;

namespace Tests.MappTests
{
    public class MappLicenseTests
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            config.AssertConfigurationIsValid();
        }


        [Fact]
        public void AutoMapper_ConvertFromLicense_IsValid()
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
        public void AutoMapper_ConvertFromLicense_ToLicenseWithBranch_IsValid()
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
