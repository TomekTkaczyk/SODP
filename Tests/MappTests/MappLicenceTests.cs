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
    public class MappLicenceTests
    {
        [Fact]
        public void AutoMapper_Configuration_IsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            config.AssertConfigurationIsValid();
        }


        [Fact]
        public void AutoMapper_ConvertFromLicence_IsValid()
        {
            var fixture = new Fixture();
            fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            LicenceDTO dto = fixture.Create<LicenceDTO>();
            Licence entity = fixture.Create<Licence>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            var mapper = config.CreateMapper();
            var licenceDTO = mapper.Map<Licence, LicenceDTO>(entity);

            Assert.NotEmpty(licenceDTO.Branches);
            Assert.True(licenceDTO.Branches.Count > 0);
        }
    }
}
