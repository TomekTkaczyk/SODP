using Microsoft.Extensions.Configuration;
using SODP.Domain.Managers;
using SODP.Model.Enums;

namespace SODP.Infrastructure.Managers;

public class FolderConfigurator : IFolderConfigurator
{
	public string ActiveFolder { get; private set; }
	public string ArchiveFolder { get; private set; }

	public FolderConfigurator(IConfiguration configuration)
	{
		var OSPrefix = $"{Environment.OSVersion.Platform}Settings:";
		ActiveFolder = configuration.GetSection($"{OSPrefix}ActiveFolder").Value;
		ArchiveFolder = configuration.GetSection($"{OSPrefix}ArchiveFolder").Value;
	}

	public string GetProjectFolder(ProjectsFolder source)
	{
		return source switch
		{
			ProjectsFolder.Archive => ArchiveFolder,
			ProjectsFolder.Active => ActiveFolder,
			_ => ActiveFolder,
		};
	}
}
