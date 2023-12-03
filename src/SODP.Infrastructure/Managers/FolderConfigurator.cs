using Microsoft.Extensions.Configuration;
using SODP.Application.Abstractions;
using SODP.Shared.Enums;

namespace SODP.Infrastructure.Managers;

public class FolderConfigurator : IFolderConfigurator
{
	public string ActiveFolder { get; init; }
	public string ArchiveFolder { get; init; }

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
			ProjectsFolder.Active => ActiveFolder,
			ProjectsFolder.Archive => ArchiveFolder,
			_ => throw new NotImplementedException(),
		};
	}
}
