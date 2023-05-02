using SODP.Shared.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Abstractions;

public interface IFolderCommandCreator
{
	string GetCommandRenameFolder(string folder, string oldName);
	string GetCommandRenameFolder(string folder, string oldName, ProjectsFolder source);

	string GetCommandCreateFolder(string folder);
	string GetCommandRestoreFolder(string folder);
	string GetCommandDeleteFolder(string folder);
	string GetCommandArchiveFolder(string folder);

	Task<string> RunCommand(string command, CancellationToken cancellationToken);
}
