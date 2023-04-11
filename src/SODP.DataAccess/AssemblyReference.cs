using System.Reflection;

namespace SODP.DataAccess;

public static class AssemblyReference
{
	public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
