﻿using SODP.Shared.Enums;

namespace SODP.Infrastructure.Managers
{
	public interface IFolderConfigurator
	{
		string ActiveFolder { get; }
		string ArchiveFolder { get; }

		string GetProjectFolder(ProjectsFolder source);
	}
}