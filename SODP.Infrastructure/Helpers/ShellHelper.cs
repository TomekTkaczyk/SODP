using Microsoft.Extensions.Logging;
using SODP.Infrastructure.Managers;
using System;
using System.Diagnostics;

public static class ShellHelper
{
    private static string commandProcessor = getOsPlatform();
    public static string CommandProcessor
    {
        get
        {
            return commandProcessor;
        }
        set
        {
            commandProcessor = value;
        }
    }

    public static string RunShell(this string cmd, ILogger logger)
    {
        var startInfo = new ProcessStartInfo()
        {
            FileName = commandProcessor,
            Arguments = cmd,
            UseShellExecute = false,
            RedirectStandardOutput = true,
        };

		var p = new Process
		{
			StartInfo = startInfo
		};
        if (p.Start())
        {
		    logger.LogInformation($"[RunShell,command started] : {commandProcessor} {cmd} ");
		}
		else
		{
		    logger.LogError($"[RunShell,command fail] : {commandProcessor} {cmd} ");
		}
		p.WaitForExit();

		return commandProcessor + cmd;
    }

    private static string getOsPlatform()
    {
        string result;
        var os = Environment.OSVersion;
       
        switch (os.Platform)
        {
            case PlatformID.Win32NT:
                result = "cmd ";
                break;
            case PlatformID.Unix:
                result = "/bin/bash";
                break;
            default:
                result = os.VersionString;
                break;
        }
        return result;

    }
}
