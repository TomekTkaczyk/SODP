using SODP.Shared.Enums;

namespace SODP.Application.Abstractions;

public interface IFolderConfigurator
{
	string ActiveFolder { get; }
	string ArchiveFolder { get; }

	string GetProjectFolder(ProjectsFolder source);
}
