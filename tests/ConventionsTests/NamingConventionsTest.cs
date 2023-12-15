using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace tests.ConventionsTests;

public class NamingConventionsTest
{
	[Fact]
	public void each_interface_name_starts_with_capital_I()
	{
		var interfaces = Utils.ConventionsHelper.interfaces();

		Assert.NotEmpty(interfaces);

		var interfaces_not_starting_with_I = interfaces
			.Where(x => !x.Name.StartsWith("I"));

		Assert.Empty(interfaces_not_starting_with_I);
	}

	[Fact]
	public void each_controller_name_ends_with_Controller()
	{
		IEnumerable<Type> controllers = Utils.ConventionsHelper.classes()
			.Where(x => x.IsSubclassOf(typeof(ControllerBase)))
			.ToList();

		Assert.NotEmpty(controllers);

		var controllers_not_end_with_Controller = controllers
			.Where(x => 
				(!x.IsGenericType && !x.Name.EndsWith("Controller")) ||
				(x.IsGenericType && !x.Name.EndsWith("Controller`1"))
				)
			.ToList();

		Assert.Empty(controllers_not_end_with_Controller);
	}
}
