using Microsoft.SqlServer.Server;
using SODP.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace tests.ConventionsTests
{
	public class StructureConventionsTest
	{
		[Fact]
		public void each_class_in_DataAccess_namespace_implement_interface()
		{
			var types = SODP.DataAccess.AssemblyReference.Assembly.GetTypes();
			var classes = types
				.Where(type => !type.IsAbstract && type.GetInterfaces().Contains(typeof(IActiveStatus)));

		}
	}
}
