using SODP.Shared.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Abstractions;

public interface IFolderCommandCreator
{
	string GetCommandCreateFolder(ProjectsFolder folder, string name);
	string GetCommandRenameFolder(ProjectsFolder folder, string oldName, string newName);
	string GetCommandDeleteFolder(ProjectsFolder folder, string name);

	string GetCommandArchiveFolder(string name);
	string GetCommandRestoreFolder(string name);

	Task<string> RunCommand(string command, CancellationToken cancellationToken);
}
