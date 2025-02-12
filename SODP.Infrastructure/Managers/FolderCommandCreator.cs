﻿using Microsoft.Extensions.Configuration;
using SODP.Domain.Managers;
using SODP.Infrastructure.Helpers;
using SODP.Model.Enums;

namespace SODP.Infrastructure.Managers;

public class FolderCommandCreator : IFolderCommandCreator
{
	private readonly IConfiguration _configuration;
	private readonly IFolderConfigurator _folderConfigurator;

	public FolderCommandCreator(IConfiguration configuration, IFolderConfigurator folderConfigurator)
	{
		_configuration = configuration;
		_folderConfigurator = folderConfigurator;
	}

	public string GetCommandCreateFolder(string folder)
	{
		return $"{GetCommand(FolderCommands.Create)} {_folderConfigurator.ActiveFolder} {folder}";
	}

	public string GetCommandRenameFolder(string folder, string oldName)
	{
		return GetCommandRenameFolder(folder, oldName, ProjectsFolder.Active);
	}

	public string GetCommandRenameFolder(string folder, string oldName, ProjectsFolder source)
	{

		return $"{GetCommand(FolderCommands.Rename)} {_folderConfigurator.GetProjectFolder(source)} {oldName} {folder}";
	}

	public string GetCommandArchiveFolder(string folder)
	{
		return $"{GetCommand(FolderCommands.Archive)} {_folderConfigurator.ActiveFolder} {_folderConfigurator.ArchiveFolder} {folder}";
	}

	public string GetCommandRestoreFolder(string folder)
	{
		return $"{GetCommand(FolderCommands.Restore)} {_folderConfigurator.ArchiveFolder} {_folderConfigurator.ActiveFolder} {folder}";
	}

	public string GetCommandDeleteFolder(string folder)
	{
		return $"{GetCommand(FolderCommands.Delete)} {_folderConfigurator.ActiveFolder} {folder}";
	}

	private string GetCommand(FolderCommands command)
	{
		var OSPrefix = $"{Environment.OSVersion.Platform}Settings:";

		var result = _configuration.GetSection($"{OSPrefix}{command}Command").Value;

		return result;
	}

	public async Task<string> RunCommand(string command)
	{
		var result = await Task.Run(() => command.RunShell());
		return result;
	}
}
