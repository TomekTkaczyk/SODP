using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;


namespace tests.Utils;

public static class ConventionsHelper
{
	public static IEnumerable<Assembly> assemblies()
	{
		var sodpDllFiles = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "SODP" + "*.dll");

		foreach (string filePath in sodpDllFiles)
		{
			yield return AppDomain.CurrentDomain.Load(Path.GetFileNameWithoutExtension(filePath));
		}
	}

	public static IEnumerable<Type> types() 
	{
		return assemblies().SelectMany(x => x.GetTypes());
	}

	public static IEnumerable<Type> classes()
	{
		return types()
			.Where(x => x.IsClass);
	}

	public static IEnumerable<Type> interfaces()
	{
		return types()
			.Where(x => x.IsInterface);
	}
}
