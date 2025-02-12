﻿using Microsoft.Extensions.Configuration;
using SODP.DataAccess;
using SODP.Domain.Managers;
using SODP.Model;
using SODP.Model.Enums;
using SODP.Shared.Enums;
using System.Text.Json;

namespace SODP.Infrastructure
{
	public class DataInitializer
    {
        private readonly IConfiguration _configuration;
        private readonly SODPDBContext _context;
        private readonly IFolderCommandCreator _folderCommandCreator;

        public DataInitializer(
            IConfiguration configuration, 
            SODPDBContext context, 
            IFolderCommandCreator folderCommandCreator)
        {
            _configuration = configuration;
            _context = context;
            _folderCommandCreator = folderCommandCreator;
        }

        public void LoadData()
        {
            if (!_context.Stages.Any())
            {
                LoadStagesFromJSON();
            }
            if(!_context.Projects.Any())
            {
                ImportProjectsFromStore();
            }
        }

        private void LoadStagesFromJSON()
        {
            var _settingsPrefix = $"{Environment.OSVersion.Platform}Settings:";
            var jsonStages = _configuration.GetSection($"{_settingsPrefix}InitStagesJSON").Value;
            if ((_context.Stages.Count() == 0) && (File.Exists(jsonStages)))
            {
                var file = File.ReadAllText(jsonStages);
                var stages = JsonSerializer.Deserialize<List<Stage>>(file);
                _context.AddRange(stages);
                _context.SaveChanges();
            }
        }

        private void ImportProjectsFromStore()
        {
            var _settingsPrefix = $"{Environment.OSVersion.Platform}Settings:";
            if (File.Exists(@"./aktualne.lst"))
            {
                CreateFolders(@"./aktualne.lst", _configuration.GetSection($"{_settingsPrefix}ActiveFolder").Value);
            }
            if (File.Exists(@"./zakonczone.lst"))
            {
                CreateFolders(@"./zakonczone.lst", _configuration.GetSection($"{_settingsPrefix}ArchiveFolder").Value);
            }
            ImportProjectsFromStore(_configuration.GetSection($"{_settingsPrefix}ActiveFolder").Value, ProjectStatus.Active);
            ImportProjectsFromStore(_configuration.GetSection($"{_settingsPrefix}ArchiveFolder").Value, ProjectStatus.Archival);
        }

        private void CreateFolders(string folderlist, string destination)
        {
            var stream = new StreamReader($@"./{folderlist}");
            string line;
            while ((line = stream.ReadLine()) != null)
            {
                Directory.CreateDirectory(destination + line);
            }
            stream.Close();
        }

        private void ImportProjectsFromStore(string projectsFolder, ProjectStatus status)
        {
            if(!Directory.Exists(projectsFolder))
            {
                Directory.CreateDirectory(projectsFolder);
            }
            var directory = Directory.EnumerateDirectories(projectsFolder);
            var validator = new ProjectNameValidator();
            foreach (var item in directory)
            {
                try
                {
                    if (!validator.Validate(item))
                    {
                        continue;
                    }
                    var currentFolder = Path.GetFileName(item);
                    var currentProject = new Project(currentFolder)
                    {
                        Status = status
                    };

                    if (!currentProject.ToString().ToUpper().Equals(currentFolder.ToUpper()))
                    {
                        var command = _folderCommandCreator.GetCommandRenameFolder(currentProject.ToString(), currentFolder, (ProjectsFolder)status);
                        var message = _folderCommandCreator.RunCommand(command).Result;
                    }

                    var stage = _context.Stages.SingleOrDefault(x => x.Sign == currentProject.Stage.Sign);
                    if (stage == null)
                    {
                        stage = new Stage(currentProject.Stage.Sign);
                    }
                    currentProject.Stage = stage;

                    var project = _context.Projects.SingleOrDefault(x => x.Number == currentProject.Number && x.Stage.Sign == currentProject.Stage.Sign);
                    if (project == null)
                    {
                        _context.Projects.Add(currentProject);
                        _context.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                }
            }
        }
    }
}
