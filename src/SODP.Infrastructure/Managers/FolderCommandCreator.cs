using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SODP.Application.Abstractions;
using SODP.Shared.Enums;

namespace SODP.Infrastructure.Managers;

public class FolderCommandCreator : IFolderCommandCreator
{
	private readonly IConfiguration _configuration;
	private readonly IFolderConfigurator _folderConfigurator;
	private readonly ILogger<IFolderCommandCreator> _logger;

	public FolderCommandCreator(IConfiguration configuration, IFolderConfigurator folderConfigurator, ILogger<IFolderCommandCreator> logger)
	{
		_configuration = configuration;
		_folderConfigurator = folderConfigurator;
		_logger = logger;
	}

	public string GetCommandCreateFolder(ProjectsFolder folder, string name)
	{
		return $"{GetCommand(FolderCommands.Create)} {_folderConfigurator.GetProjectFolder(folder)} {name}";
	}

	public string GetCommandRenameFolder(ProjectsFolder folder, string oldName, string newName)
	{

		return $"{GetCommand(FolderCommands.Rename)} {_folderConfigurator.GetProjectFolder(folder)} {oldName} {newName}";
	}

	public string GetCommandArchiveFolder(string name)
	{
		return $"{GetCommand(FolderCommands.Archive)} {_folderConfigurator.ActiveFolder} {_folderConfigurator.ArchiveFolder} {name}";
	}

	public string GetCommandRestoreFolder(string name)
	{
		return $"{GetCommand(FolderCommands.Restore)} {_folderConfigurator.ArchiveFolder} {_folderConfigurator.ActiveFolder} {name}";
	}

	public string GetCommandDeleteFolder(ProjectsFolder folder, string name)
	{
		return $"{GetCommand(FolderCommands.Delete)} {_folderConfigurator.GetProjectFolder(folder)} {name}";
	}

	private string GetCommand(FolderCommands command)
	{
		var OSPrefix = $"{Environment.OSVersion.Platform}Settings:";

		var result = _configuration.GetSection($"{OSPrefix}{command}Command").Value;

		return result;
	}

	public async Task<string> RunCommand(string command, CancellationToken cancellationToken)
	{
		return await Task.Run(() => command.RunShell(_logger), cancellationToken);
	}
}
