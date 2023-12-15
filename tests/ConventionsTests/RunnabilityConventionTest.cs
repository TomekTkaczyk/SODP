using System;
using System.Collections.Generic;
using System.Linq;
using tests.Utils;
using Xunit;

namespace tests.ConventionsTests;

public class RunnabilityConventionTest
{
	[Fact]
	public void each_interface_has_implementation()
	{
		var interfaces = ConventionsHelper.interfaces();

		var concrete_types = ConventionsHelper.classes()
			.Where(x => !x.IsAbstract)
			.ToList();

		var interfacesWithoutImplementation = new List<Type>();

		foreach (var @interface in interfaces)
		{
			var types_assignable_to_interface = concrete_types
				.Where(x => @interface.IsAssignableFrom(x));

			if (!types_assignable_to_interface.Any())
			{
				interfacesWithoutImplementation.Add(@interface);
			}
		}

		if (interfacesWithoutImplementation.Any())
		{
			string interfacesList = string.Join(", ", interfacesWithoutImplementation.Select(i => i.ToString()));
			Assert.Fail($"Interfaces without implementation: {interfacesList}");
		}
	}

	[Fact]
	public void each_cqrs_request_implementing_the_IRequest_interface_is_of_type_record()
	{
	}
}
