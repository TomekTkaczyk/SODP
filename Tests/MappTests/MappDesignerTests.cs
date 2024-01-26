using AutoFixture;
using AutoMapper;
using SODP.Domain;
using SODP.Domain.Entities;
using SODP.Shared.DTO;
using System.Linq;
using Xunit;

namespace tests.MappTests;

public class MappDesignerTests
{
    [Fact]
    internal void AutoMapper_ConvertFromDesigner_IsValid()
    {
        var fixture = new Fixture();
        fixture.Behaviors
            .OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));

        fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        Designer entity = fixture.Create<Designer>();

        var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
        var mapper = config.CreateMapper();
        var designerDTO = mapper.Map<Designer, DesignerDTO>(entity);

        Assert.Equal(entity.Title.Value,designerDTO.Title);
        Assert.Equal(entity.Firstname.Value, designerDTO.Firstname);
        Assert.Equal(entity.Lastname.Value, designerDTO.Lastname);
    }
}
