using SODP.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests.Model;

public class ProjectTests
{
	public IReadOnlyCollection<string> Collection { get; set; }

	[Fact]
	public void when_add_ProjectPart_to_Project_ProjectPartCollection_has_one_element()
	{
		var project = Project.Create("1111",Stage.Create("PB",""),"Project");

        project.AddPart("PB", "PROJEKT BUDOWLANY");

		Assert.Collection(project.Parts, part => Assert.True(part.Sign.Equals("PB")));
	}
}
