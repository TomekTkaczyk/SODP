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
    public class MappDesignerTests
    {
        [Fact]
        public void AutoMapper_ConvertFromDesigner_IsValid()
        {
            var fixture = new Fixture();
            fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            DesignerDTO dto = fixture.Create<DesignerDTO>();
            Designer entity = fixture.Create<Designer>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            var mapper = config.CreateMapper();
            var projectDTO = mapper.Map<Designer, DesignerDTO>(entity);

            Assert.NotEmpty(projectDTO.Title);
            Assert.NotEmpty(projectDTO.Firstname);
            Assert.NotEmpty(projectDTO.Lastname);
        }

        [Fact]
        public void AutoMapper_ConvertFromDesignerDTO_IsValid()
        {
            var fixture = new Fixture();
            fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            DesignerDTO dto = fixture.Create<DesignerDTO>();
            Designer entity = fixture.Create<Designer>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            var mapper = config.CreateMapper();
            var designer = mapper.Map<DesignerDTO, Designer>(dto);

            Assert.NotEmpty(designer.Title);
            Assert.NotEmpty(designer.Firstname);
            Assert.NotEmpty(designer.Lastname);
        }
    }
}
