using AutoFixture;
using AutoMapper;
using SODP.Domain;
using SODP.Model;
using SODP.Shared.DTO;
using System.Linq;
using Xunit;

namespace Tests.MappTests
{
    public class MappProjectTests
    {
        private readonly MapperConfiguration _config;
        private readonly IMapper _mapper;

        public MappProjectTests()
        {
            _config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
            _mapper = _config.CreateMapper();
        }
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

            var projectDTO = _mapper.Map<Project, ProjectDTO>(entity);

            Assert.NotEmpty(projectDTO.Branches);
            Assert.True(projectDTO.Branches.Count > 0);
        }

        [Fact]
        public void When_project_given_null_strings_then_should_by_return_estring_empty_fields()
        {
            var dto = new ProjectDTO();

            var entity = _mapper.Map<ProjectDTO, Project>(dto);

            Assert.NotNull(entity.Title);
            Assert.NotNull(entity.Address);
            Assert.NotNull(entity.LocationUnit);
            Assert.NotNull(entity.Investor);
            Assert.NotNull(entity.Description);
        }
    }
}
