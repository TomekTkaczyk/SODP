using SODP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace tests.ConventionsTests;

public class TestsConventionsTest
{
	[Fact]
	public void each_test_is_in_tests_namespace()
	{
		var types = typeof(TestsConventionsTest).Assembly.GetTypes();

		var testClasses = types
			.Where(type => !string.IsNullOrEmpty(type.Namespace) && !type.Namespace.StartsWith("tests."))
			.ToList();

		Assert.Empty(testClasses);
	}

	[Fact]
	public void each_test_class_is_public()
	{
		var types = typeof(TestsConventionsTest).Assembly.GetTypes();

		Assert.NotEmpty(types);

		var testClasses = types
			.Where(type => !type.IsAbstract && type.IsClass && !type.IsPublic)
			.ToList();

		Assert.Empty(testClasses);
	}
}
