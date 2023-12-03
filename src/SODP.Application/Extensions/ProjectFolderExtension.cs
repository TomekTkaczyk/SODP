using SODP.Shared.Enums;
using System;

namespace SODP.Application.Extensions;

public static class ProjectFolderExtension
{
	public static ProjectsFolder GetProjectsFolder(this ProjectStatus status)
	{
		return status switch
		{
			ProjectStatus.Active => ProjectsFolder.Active,
			ProjectStatus.Archival => ProjectsFolder.Archive,
			_ => throw new NotImplementedException(),
		};
	} 		 
}
