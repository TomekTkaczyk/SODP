using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xunit;

namespace tests.ConventionsTests;

public class TestsConventionsTests
{
	[Fact]
	internal void each_test_is_in_tests_namespace()
	{
		var types = GetxUnitTestClasses();
		
		Assert.NotEmpty(types);
		
		var tests_that_namespace_not_starts_with_tests = types
			.Where(type => string.IsNullOrEmpty(type.Namespace) && !type.Namespace.StartsWith("tests."))
			.ToList();

		Assert.Empty(tests_that_namespace_not_starts_with_tests);
	}

	[Fact]
	internal void each_test_class_name_ends_with_Tests()
	{
		var types = GetxUnitTestClasses();

		Assert.NotEmpty(types);

		var tests_that_name_not_ends_with_Test = types
			.Where(type => !type.Name.EndsWith("Tests"))
			.ToList();

		Assert.Empty(tests_that_name_not_ends_with_Test);
	}

	[Fact]
	internal void each_test_methods_is_internal()
	{
		var methods = GetxUnitTestClasses()
			.SelectMany(x => GetxUnitTestMethods(x));

		Assert.NotEmpty(methods);

		var metods_that_are_not_internal = methods
			.Where(x => !x.IsAssembly)
			.ToList();					  

		Assert.Empty(metods_that_are_not_internal);
	}

	[Fact]
	internal void each_test_methods_return_void_or_Task()
	{
		var methods = GetxUnitTestClasses()
			.SelectMany(x => GetxUnitTestMethods(x));

		Assert.NotEmpty(methods);

		var metods_that_are_return_value_or_task = methods
			.Where(x => x.ReturnType != typeof(void) && x.ReturnType != typeof(Task))
			.ToList();

		Assert.Empty(metods_that_are_return_value_or_task);
	}


	private IEnumerable<MethodInfo> GetxUnitTestMethods(Type type)
	{
		return type.GetMethods(
				BindingFlags.Public |
				BindingFlags.NonPublic |
				BindingFlags.Instance)
			.Where(x => x.GetCustomAttribute<FactAttribute>() != null ||
						x.GetCustomAttribute<TheoryAttribute>() != null);
	}

	private IEnumerable<Type> GetxUnitTestClasses()
	{
		return ConventionsHelper.Types(Assembly.GetExecutingAssembly())
			.Where(x => GetxUnitTestMethods(x).Any());
	}
}
