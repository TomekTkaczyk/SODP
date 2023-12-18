using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;


namespace tests.ConventionsTests;

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

	public static IEnumerable<Type> types(Assembly assembly)
	{
		return assembly.GetTypes();
	}

	public static IEnumerable<Type> classes()
    {
        return types().Where(x => x.IsClass);
    }

	public static IEnumerable<Type> records()
	{
		return classes().Where(x => x.GetMethod("<Clone>$") != null);
	}

	public static IEnumerable<Type> interfaces()
    {
        return types().Where(x => x.IsInterface);
    }

	public static IEnumerable<MethodInfo> methods()
	{
		return classes()
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
