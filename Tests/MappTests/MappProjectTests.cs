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
    public class MappProjectTests
    {
        [Fact]
        public void AutoMapper_ConvertFromProject_IsValid()
        {
            var fixture = new Fixture();
            fixture.Behaviors
                .OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));

            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            ProjectDTO dto = fixture.Create<ProjectDTO>();
            Project entity = fixture.Create<Project>();

            var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            var mapper = config.CreateMapper();
            var projectDTO = mapper.Map<Project, ProjectDTO>(entity);

            Assert.NotEmpty(projectDTO.Branches);
            Assert.True(projectDTO.Branches.Count > 0);
        }
    }
}
