using SODP.Domain.Entities;
using SODP.Domain.Exceptions.ProjectExceptions;
using Xunit;

namespace tests.Model;

public class ProjectTests
{

	[Fact]
	internal void when_add_ProjectPart_to_Project_ProjectPartCollection_has_one_element()
	{
		var project = Project.Create("1111",Stage.Create("PB",""),"Project");

        project.AddPart("PB", "PROJEKT BUDOWLANY");

		Assert.Single(project.Parts, part => part.Sign.Equals("PB"));
	}

	[Fact]
	internal void when_add_second_same_ProjectPart_to_Project_throw_exception()
	{
		var project = Project.Create("1111", Stage.Create("PB", ""), "Project");

		project.AddPart("PB", "PROJEKT BUDOWLANY");

		Assert.Throws<ProjectPartConflictException>(() => project.AddPart("PB", "PROJEKT BUDOWLANY"));
	}
}
