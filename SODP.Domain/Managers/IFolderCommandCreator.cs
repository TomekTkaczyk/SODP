using SODP.Model.Enums;
using System.Threading.Tasks;

namespace SODP.Domain.Managers
{
    public interface IFolderCommandCreator
    {
        string GetCommandRenameFolder(string folder, string oldName);
        string GetCommandRenameFolder(string folder, string oldName, ProjectsFolder source);
        
        string GetCommandCreateFolder(string folder);
        string GetCommandRestoreFolder(string folder);
        string GetCommandDeleteFolder(string folder);
        string GetCommandArchiveFolder(string folder);

        Task<string> RunCommand(string command);
    }
}
