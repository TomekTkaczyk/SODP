using SODP.Domain.Managers;
using SODP.Model;
using SODP.Model.Enums;
using SODP.Model.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SODP.Infrastructure.Managers
{
    public class FolderManager : IFolderManager
    {
        private readonly IFolderCommandCreator _folderCommandCreator;
        private readonly FolderConfigurator _folderConfigurator;

        public FolderManager(FolderConfigurator folderConfigurator, IFolderCommandCreator folderCommandCreator)
        {
            _folderCommandCreator = folderCommandCreator;
            _folderConfigurator = folderConfigurator;
        }

        public async Task<(bool Success, string Message)> CreateFolderAsync(Project project)
        {
            string command;
            string message;
            string folder = project.ToString();
            var catalog = GetMatchingFolders(_folderConfigurator.ActiveFolder, project);

            switch (catalog.Count())
            {
                case 0:
                    command = _folderCommandCreator.GetCommandCreateFolder(folder);
                    message = await _folderCommandCreator.RunCommand(command);

                    return (Directory.Exists(_folderConfigurator.ActiveFolder + folder), $"{command} {message}");
                case 1:
                    command = _folderCommandCreator.GetCommandRenameFolder(folder, catalog[0]);
                    message = await _folderCommandCreator.RunCommand(command);

                    return (Directory.Exists(_folderConfigurator.ActiveFolder + folder), $"{command} {message}");
                default:
                    return (false, $"Istnieje więcej niż 1 folder projektu {project.Symbol}");
            }
        }

        public async Task<(bool Success, string Message)> RenameFolderAsync(Project project, ProjectsFolder source)
        {
            string folder = project.ToString();
            var catalog = GetMatchingFolders(_folderConfigurator.GetProjectFolder(source), project);
            switch (catalog.Count())
            {
                case 0:
                    return await CreateFolderAsync(project);
                case 1:
                    var command = _folderCommandCreator.GetCommandRenameFolder(folder, catalog[0], source);
                    var message = await _folderCommandCreator.RunCommand(command);

                    return (Directory.Exists(_folderConfigurator.GetProjectFolder(source) + folder), $"{command} {message}");
                default:
                    return (false, $"Istnieje więcej niż 1 folder projektu {project.Symbol}");
            }
        }

        public async Task<(bool Success, string Message)> RenameFolderAsync(Project project, string oldName, ProjectsFolder source)
        {
            string folder = project.ToString();
            var command = _folderCommandCreator.GetCommandRenameFolder(folder, oldName, source);
            var message = await _folderCommandCreator.RunCommand(command);

            return (Directory.Exists(_folderConfigurator.GetProjectFolder(source) + folder), $"{command} {message}");
        }

        public async Task<(bool Success, string Message)> DeleteFolderAsync(Project project)
        {
            
            var folders = GetMatchingFolders(_folderConfigurator.ActiveFolder, project);
            switch(folders.Count())
            {
                case 0:
                    return(true, $"Folder projektu {project.Symbol} nie istnieje.");
                case 1:
                    if (!FolderIsEmpty($"{_folderConfigurator.ActiveFolder}{folders[0]}"))
                    {
                        return (false,$"Folder projektu {project.Symbol} nie jest pusty.");
                    }
                    var command = _folderCommandCreator.GetCommandDeleteFolder(folders[0]);
                    var message = await _folderCommandCreator.RunCommand(command);

                    return (!Directory.Exists(_folderConfigurator.ActiveFolder + folders[0]), $"{command} {message}");
                default:
                    return (false, $"Istnieje więcej niż 1 folder projektu {project.Symbol}");
            }
        }

        public async Task<(bool Success, string Message)> ArchiveFolderAsync(Project project)
        {
            string folder = project.ToString();
            (bool Success, string Message) result;
            var folders = GetMatchingFolders(_folderConfigurator.ActiveFolder, project);
            switch(folders.Count())
            {
                case 0:
                    return(false, $"Folder projektu {project.Symbol} nie istnieje.");
                case 1:
                    if(FolderIsEmpty(_folderConfigurator.ActiveFolder + folders[0]))
                    {
                        return (false, $"Folder projektu {project.Symbol} jest pusty.");
                    }
                    if(!folders[0].Equals(project.ToString()))
                    {
                        result = await RenameFolderAsync(project, ProjectsFolder.Active);
                        if(!result.Success)
                        {
                            return result;
                        }
                    }
                    var command = _folderCommandCreator.GetCommandArchiveFolder(folder);
                    var message = await _folderCommandCreator.RunCommand(command);

                    return (Directory.Exists(_folderConfigurator.ArchiveFolder + folder), $"{command} {message}");
                default:
                    return (false, $"Istnieje więcej niż 1 folder projektu {project.Symbol}");
            }
        }

        public async Task<(bool Success, string Message)> RestoreFolderAsync(Project project)
        {
            string folder = project.ToString();
            (bool Success, string Message) result;
            var catalog = GetMatchingFolders(_folderConfigurator.ArchiveFolder, project);
            switch(catalog.Count())
            {
                case 0:
                    return(false, $"Folder projektu {project.Symbol} nie istnieje.");
                case 1:
                    if (!catalog[0].Equals(project.ToString()))
                    {
                        result = await RenameFolderAsync(project, ProjectsFolder.Archive);
                        if (!result.Success)
                        {
                            return result;
                        }
                    }
                    var command = _folderCommandCreator.GetCommandRestoreFolder(folder);
                    var message = await _folderCommandCreator.RunCommand(command);

                    return (Directory.Exists(_folderConfigurator.ActiveFolder + folder), $"{command} {message}");
                default:
                    return (false, $"Istnieje więcej niż 1 folder projektu {project.Symbol}");
            }
        }

        private bool FolderIsEmpty(string folder)
        {
            return Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories).Length == 0;
        }

        private IList<string> GetMatchingFolders(string projectFolder, Project project)
        {
            return Directory.EnumerateDirectories(projectFolder)
                .Select(x => Path.GetFileName(x))
                .Where(n => {
                    var symbol = n.GetUntilOrEmpty("_");
                    return (!String.IsNullOrEmpty(symbol) && symbol.Equals(project.Symbol));
                })
                .ToList();
        }
    }
}
