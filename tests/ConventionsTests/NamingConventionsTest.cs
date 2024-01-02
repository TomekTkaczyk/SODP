using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using SODP.Domain.Attributes;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xunit;

namespace tests.ConventionsTests;

public class NamingConventionsTests
{
	[Fact]
	internal void each_interface_name_starts_with_capital_I_and_second_char_is_capital_letter()
	{
		var interfaces = ConventionsHelper.Interfaces();

		Assert.NotEmpty(interfaces);

		var regex = new Regex(@"^I[A-Z]"); 

		var interfaces_not_starting_with_I_or_second_char_is_not_capital = interfaces
			.Where(x => !regex.Match(x.Name).Success);

		Assert.Empty(interfaces_not_starting_with_I_or_second_char_is_not_capital);
	}

	[Fact]
	internal void each_controller_name_ends_with_Controller()
	{
		var controllers = ConventionsHelper.Classes()
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

	[Fact]
	internal void each_async_method_name_ends_with_Async()
	{
		var methods = ConventionsHelper.Methods()
			.Where(x => x.GetCustomAttribute<AsyncStateMachineAttribute>() != null)
			.Where(x => (x.ReturnType.BaseType == typeof(Task) || x.ReturnType.BaseType == typeof(ValueTask)))
			.Where(x => !x.IsDefined(typeof(IgnoreMethodAsyncNameConventionAttribute)));

		Assert.NotEmpty(methods);

		var methods_not_ends_with_Asyns = methods
			.Where(x => !x.Name.EndsWith("Async"))
			.Select(x => new { x.DeclaringType.FullName,x.Name})
			.ToList();

		Assert.Empty(methods_not_ends_with_Asyns);
	}
}
