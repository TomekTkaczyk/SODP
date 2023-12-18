using SODP.Domain.Entities;
using System;
using System.Linq;
using Xunit;

namespace tests.ConventionsTests;

public class StructureConventionsTests
{
	[Fact]
	internal void each_class_in_DataAccess_namespace_implement_interface()
	{
		var types = SODP.DataAccess.AssemblyReference.Assembly.GetTypes();
		var classes = types
			.Where(type => !type.IsAbstract && type.GetInterfaces().Contains(typeof(IActiveStatus)));

	}
}
