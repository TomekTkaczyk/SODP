using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;


namespace tests.ConventionsTests;

public static class ConventionsHelper
{

	public static IEnumerable<Assembly> Assemblies()
    {
        var sodpDllFiles = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "SODP" + "*.dll");

        foreach (string filePath in sodpDllFiles)
        {
            yield return AppDomain.CurrentDomain.Load(Path.GetFileNameWithoutExtension(filePath));
        }
    }

    public static IEnumerable<Type> Types()
    {
        return Assemblies().SelectMany(x => x.GetTypes());
    }

	public static IEnumerable<Type> Types(Assembly assembly)
	{
		return assembly.GetTypes();
	}

	public static IEnumerable<Type> Classes()
    {
        return Types().Where(x => x.IsClass);
    }

	public static IEnumerable<Type> Records()
	{
		return Classes().Where(x => x.GetMethod("<Clone>$") != null);
	}

	public static IEnumerable<Type> Interfaces()
    {
        return Types().Where(x => x.IsInterface);
    }

	public static IEnumerable<MethodInfo> Methods()
	{
		return Classes()
            .SelectMany(x => x.GetMethods(
                BindingFlags.NonPublic |
                BindingFlags.Public |
                BindingFlags.Instance |
                BindingFlags.Static |
                BindingFlags.DeclaredOnly
                ));
	}

	public static bool IsRecord(this Type type) => type.GetMethod("<Clone>$") != null;
}
