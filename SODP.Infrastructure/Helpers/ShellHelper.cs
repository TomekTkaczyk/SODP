using System.Diagnostics;

namespace SODP.Infrastructure.Helpers;

public static class ShellHelper
{
	public static string RunShell(this string cmd)
	{
		var p = new Process();

		p.StartInfo.FileName = GetCommandProcessor();
		p.StartInfo.Arguments = GetCommandArgument(cmd);
		p.StartInfo.UseShellExecute = false;
		p.StartInfo.RedirectStandardOutput = true;
		p.StartInfo.RedirectStandardError = true;

		p.Start();

		string output = p.StandardOutput.ReadToEnd();
		string error = p.StandardError.ReadToEnd();

		p.WaitForExit();

		return string.IsNullOrEmpty(error) ? output : $"Error: {error}";
	}

	private static string GetCommandArgument(string command)
	{
		var os = Environment.OSVersion;
		string result;
		switch(os.Platform) {
			case PlatformID.Win32NT:
				result = $"/C {command}";
				break;
			case PlatformID.Unix:
				result = $"-c \"{command}\"";
				break;
			default:
				result = "Not found";
				break;
		}
		return result;
	}

	private static string GetCommandProcessor()
	{
		string result;
		var os = Environment.OSVersion;

		switch(os.Platform) {
			case PlatformID.Win32NT:
				result = @"cmd";
				break;
			case PlatformID.Unix:
				result = @"/bin/bash";
				break;
			default:
				result = os.VersionString;
				break;
		}
		return result;
	}
}
