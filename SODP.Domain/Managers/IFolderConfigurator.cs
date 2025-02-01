using SODP.Model.Enums;

namespace SODP.Domain.Managers;
public interface IFolderConfigurator
{
	string ActiveFolder { get; }
	string ArchiveFolder { get; }

	string GetProjectFolder(ProjectsFolder source);
}