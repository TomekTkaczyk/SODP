using SODP.Domain.Managers;
using SODP.Model;
using SODP.Model.Enums;
using SODP.Model.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SODP.Application.Managers
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

        public async Task<(bool Success, string Message)> CreateFolderAsync(Project project, ProjectsFolder source = ProjectsFolder.Active)
        {
            string command;
            string message;
            var catalog = GetMatchingFolders(_folderConfigurator.GetProjectFolder(source), project);
            switch (catalog.Count())
            {
                case 0:
                    command = _folderCommandCreator.GetCommandCreateFolder(project);
                    message = await Task.Run(() => command.RunShell());

                    return (Directory.Exists(_folderConfigurator.GetProjectFolder(source) + project.ToString()), $"{command} {message}");
                case 1:
                    command = _folderCommandCreator.GetCommandRenameFolder(project, catalog[0]);
                    message = await Task.Run(() => command.RunShell());

                    return (Directory.Exists(_folderConfigurator.GetProjectFolder(source) + project.ToString()), $"{command} {message}");
                default:
                    return (false, $"Istnieje więcej niż 1 folder projektu {project.Symbol}");
            }
        }

        public async Task<(bool Success, string Message)> RenameFolderAsync(Project project, ProjectsFolder source = ProjectsFolder.Active)
        {
            var catalog = GetMatchingFolders(_folderConfigurator.GetProjectFolder(source), project);
            switch (catalog.Count())
            {
                case 0:
                    return await CreateFolderAsync(project, source);
                case 1:
                    var command = _folderCommandCreator.GetCommandRenameFolder(project, catalog[0], source);
                    var message = await Task.Run(() => command.RunShell());

                    return (Directory.Exists(_folderConfigurator.GetProjectFolder(source) + project.ToString()), $"{command} {message}");
                default:
                    return (false, $"Istnieje więcej niż 1 folder projektu {project.Symbol}");
            }
        }

        public async Task<(bool Success, string Message)> DeleteFolderAsync(Project project, ProjectsFolder source = ProjectsFolder.Active)
        {
            var catalog = GetMatchingFolders(_folderConfigurator.GetProjectFolder(source), project);
            switch(catalog.Count())
            {
                case 0:
                    return(true, $"Folder projektu {project.Symbol} nie istnieje.");
                case 1:
                    if(!FolderIsEmpty($"{_folderConfigurator.ActiveFolder}{catalog[0]}"))
                    {
                        return (false,$"Folder projektu {project.Symbol} nie jest pusty.");
                    }
                    var command = _folderCommandCreator.GetCommandDeleteFolder(project);
                    var message = await Task.Run(() => command.RunShell());

                    return (!Directory.Exists(_folderConfigurator.GetProjectFolder(source) + project.ToString()), $"{command} {message}");
                default:
                    return (false, $"Istnieje więcej niż 1 folder projektu {project.Symbol}");
            }
        }

        public async Task<(bool Success, string Message)> ArchiveFolderAsync(Project project)
        {
            (bool Success, string Message) result;
            var catalog = GetMatchingFolders(_folderConfigurator.ActiveFolder, project);
            switch(catalog.Count())
            {
                case 0:
                    return(false, $"Folder projektu {project.Symbol} nie istnieje.");
                case 1:
                    if(FolderIsEmpty(_folderConfigurator.ActiveFolder + catalog[0]))
                    {
                        return (false, $"Folder projektu {project.Symbol} jest pusty.");
                    }
                    if(!catalog[0].Equals(project.ToString()))
                    {
                        result = await RenameFolderAsync(project);
                        if(!result.Success)
                        {
                            return result;
                        }
                    }
                    var command = _folderCommandCreator.GetCommandArchiveFolder(project);
                    var message = await Task.Run(() => command.RunShell());

                    return (Directory.Exists(_folderConfigurator.ArchiveFolder + project.ToString()), $"{command} {message}");
                default:
                    return (false, $"Istnieje więcej niż 1 folder projektu {project.Symbol}");
            }
        }

        public async Task<(bool Success, string Message)> RestoreFolderAsync(Project project)
        {
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
                    var command = _folderCommandCreator.GetCommandRestoreFolder(project);
                    var message = await Task.Run(() => command.RunShell());

                    return (Directory.Exists(_folderConfigurator.ActiveFolder + project.ToString()), $"{command} {message}");
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
