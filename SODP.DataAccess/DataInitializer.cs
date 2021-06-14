using Microsoft.Extensions.Configuration;
using SODP.Domain.Managers;
using SODP.Model;
using SODP.Model.Enums;
using SODP.Model.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SODP.DataAccess
{
    public class DataInitializer
    {
        private readonly IConfiguration _configuration;
        private readonly SODPDBContext _context;
        private readonly IFolderCommandCreator _folderCommandCreator;
        private readonly IFolderManager _folderManager;

        public DataInitializer(IConfiguration configuration, SODPDBContext context, IFolderCommandCreator folderCommandCreator, IFolderManager folderManager)
        {
            _configuration = configuration;
            _context = context;
            _folderCommandCreator = folderCommandCreator;
            _folderManager = folderManager;
        }

        public void LoadData()
        {
            if (_context.Stages.Count() == 0)
            {
                LoadStagesFromJSON();
            }
            if(_context.Projects.Count() == 0)
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
            CreateFolders(@"./aktualne.lst", _configuration.GetSection($"{_settingsPrefix}ActiveFolder").Value);
            CreateFolders(@"./zakonczone.lst", _configuration.GetSection($"{_settingsPrefix}ArchiveFolder").Value);
            ImportProjectsFromStore(_configuration.GetSection($"{_settingsPrefix}ActiveFolder").Value, ProjectStatus.Active);
            ImportProjectsFromStore(_configuration.GetSection($"{_settingsPrefix}ArchiveFolder").Value, ProjectStatus.Archived);
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

                    var stage = _context.Stages.FirstOrDefault(x => x.Sign == currentProject.Stage.Sign);
                    if (stage == null)
                    {
                        stage = new Stage() { Sign = currentProject.Stage.Sign, Title = "" };
                    }
                    currentProject.Stage = stage;

                    var project = _context.Projects.FirstOrDefault(x => x.Number == currentProject.Number && x.Stage.Sign == currentProject.Stage.Sign);
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
