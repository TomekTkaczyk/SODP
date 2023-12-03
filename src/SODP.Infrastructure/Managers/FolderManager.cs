using Microsoft.Extensions.Logging;
using SODP.Application.Abstractions;
using SODP.Domain.Entities;
using SODP.Shared.Enums;
using SODP.Shared.Extensions;

namespace SODP.Infrastructure.Managers
{
	public class FolderManager : IFolderManager
    {
        private readonly IFolderCommandCreator _folderCommandCreator;
		private readonly ILogger<IFolderManager> _logger;
		private readonly IFolderConfigurator _folderConfigurator;

        private string _command;
        private string _message;

        public FolderManager(
            IFolderConfigurator folderConfigurator, 
            IFolderCommandCreator folderCommandCreator, 
            ILogger<IFolderManager> logger)
        {
            _folderCommandCreator = folderCommandCreator;
			_folderConfigurator = folderConfigurator;
			_logger = logger;
        }

        public async Task<(bool Success, string Message)> CreateProjectFolderAsync(Project project, CancellationToken cancellationToken)
        {
            string projectFolder = project.ToString();
            var matchFolders = GetMatchingFolders(ProjectsFolder.Active, project);

            switch (matchFolders.Count)
            {
                case 0:
                    _command = _folderCommandCreator.GetCommandCreateFolder(ProjectsFolder.Active, projectFolder);
                    _message = await _folderCommandCreator.RunCommand(_command, cancellationToken);
                    _logger.LogInformation("[FolderManager,Create folder] : {message}", _message);

                    return (Directory.Exists(_folderConfigurator.ActiveFolder + projectFolder), $"{_command} {_message}");
                case 1:
                    _command = _folderCommandCreator.GetCommandRenameFolder(ProjectsFolder.Active, matchFolders[0], projectFolder);
                    _message = await _folderCommandCreator.RunCommand(_command, cancellationToken);
					_logger.LogInformation("[FolderManager,Rename folder] : {message}", _message);

					return (Directory.Exists(_folderConfigurator.ActiveFolder + projectFolder), $"{_command} {_message}");
                default:
                    return (false, $"More than one folder with symbol {project.Symbol}");
            }
        }

        public async Task<(bool Success, string Message)> MatchProjectFolderAsync(Project project, CancellationToken cancellationToken)
        {
            string projectFolder = project.ToString();
            var matchFolders = GetMatchingFolders(ProjectsFolder.Active, project);
            switch (matchFolders.Count)
            {
                case 0:
                    return await CreateProjectFolderAsync(project, cancellationToken);
                case 1:
                    _command = _folderCommandCreator.GetCommandRenameFolder(ProjectsFolder.Active, matchFolders[0], projectFolder);
                    _message = await _folderCommandCreator.RunCommand(_command, cancellationToken);
					_logger.LogInformation("[FolderManager,Rename folder] : {message}", _message);

					return (Directory.Exists(_folderConfigurator.ActiveFolder + projectFolder), $"{_command} {_message}");
                default:
                    return (false, $"More than one folder with symbol {project.Symbol}");
            }
        }

        public async Task<(bool Success, string Message)> DeleteProjectFolderAsync(Project project, CancellationToken cancellationToken)
        {
            
            var matchFolders = GetMatchingFolders(ProjectsFolder.Active, project);
            switch(matchFolders.Count)
            {
                case 0:
                    return(true, $"Folder {project.Symbol} not exist.");
                case 1:
                    if (!FolderIsEmpty($"{_folderConfigurator.ActiveFolder}{matchFolders[0]}"))
                    {
                        return (false,$"Folder {project.Symbol} not empty.");
                    }
                    _command = _folderCommandCreator.GetCommandDeleteFolder(ProjectsFolder.Active, matchFolders[0]);
                    _message = await _folderCommandCreator.RunCommand(_command, cancellationToken);
					_logger.LogInformation("[FolderManager,Delete folder] : {message}", _message);

					return (!Directory.Exists(_folderConfigurator.ActiveFolder + matchFolders[0]), $"{_command} {_message}");
                default:
                    return (false, $"More than one folder with symbol {project.Symbol}.");
            }
        }

        public async Task<(bool Success, string Message)> ArchiveProjectFolderAsync(Project project, CancellationToken cancellationToken)
        {
            string projectFolder = project.ToString();
            var matchFolders = GetMatchingFolders(ProjectsFolder.Active, project);
            switch(matchFolders.Count)
            {
                case 0:
                    return(false, $"Folder {project.Symbol} not exist.");
                case 1:
                    if(FolderIsEmpty(_folderConfigurator.ActiveFolder + matchFolders[0]))
                    {
                        return (false, $"Folder {project.Symbol} is empty.");
                    }
                    if(!matchFolders[0].Equals(projectFolder))
                    {
                        var result = await MatchProjectFolderAsync(project, cancellationToken);
                        if(!result.Success)
                        {
                            return result;
                        }
                    }
                    _command = _folderCommandCreator.GetCommandArchiveFolder(projectFolder);
                    _message = await _folderCommandCreator.RunCommand(_command, cancellationToken);
					_logger.LogInformation("[FolderManager,Archive folder] : {message}", _message);

					return (Directory.Exists(_folderConfigurator.ArchiveFolder + projectFolder), $"{_command} {_message}");
                default:
                    return (false, $"More than one folder with symbol {project.Symbol}.");
            }
        }

        public async Task<(bool Success, string Message)> RestoreProjectFolderAsync(Project project, CancellationToken cancellationToken)
        {
            string projectFolder = project.ToString();
            var matchFolders = GetMatchingFolders(ProjectsFolder.Archive, project);
            switch(matchFolders.Count)
            {
                case 0:
                    return(false, $"Folder {project.Symbol} not exist.");
                case 1:
                    _command = _folderCommandCreator.GetCommandRestoreFolder(matchFolders[0]);
                    _message = await _folderCommandCreator.RunCommand(_command, cancellationToken);

					if (!matchFolders[0].Equals(projectFolder))
					{
						var result = await MatchProjectFolderAsync(project, cancellationToken);
						if (!result.Success)
						{
							return result;
						}
					}
					_logger.LogInformation("[FolderManager,Restore folder] : {message}", _message);

					return (Directory.Exists(_folderConfigurator.ActiveFolder + projectFolder), $"{_command} {_message}");
                default:
                    return (false, $"More than one folder with symbol {project.Symbol}.");
            }
        }

        private static bool FolderIsEmpty(string folder)
        {
            return Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories).Length == 0;
        }

        private IList<string> GetMatchingFolders(ProjectsFolder folder, Project project)
        {
            var projectFolder = _folderConfigurator.GetProjectFolder(folder);

            return Directory.EnumerateDirectories(projectFolder)
                .Select(x => Path.GetFileName(x))
                .Where(n => {
                    var symbol = n.GetUntilOrEmpty("_");
                    return (!string.IsNullOrEmpty(symbol) && symbol.Equals(project.Symbol));
                })
                .ToList();
        }
    }
}
