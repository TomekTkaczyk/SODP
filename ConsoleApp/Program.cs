
using System.Diagnostics;

Console.WriteLine("Hello, World!");
RunShell();
Console.WriteLine("Done.");


static void RunShell()
{
	var p = new Process();

	p.StartInfo.FileName = "cmd.exe";
	p.StartInfo.Arguments = @"/c \home\perlon\createproject.cmd \home\perlon\aktualne 1234_costam";
	p.StartInfo.UseShellExecute = false;
	p.StartInfo.RedirectStandardOutput = false;
	p.Start();

	p.WaitForExit();
}
