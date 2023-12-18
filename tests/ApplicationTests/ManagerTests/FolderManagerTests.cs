using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SODP.Application.Abstractions;
using SODP.Domain.Entities;
using SODP.Infrastructure.Managers;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace tests.ApplicationTests.ManagerTests;

public class FolderManagerTests
{
	[Fact]
	internal async Task method_create_shuld_create_new_folder()
	{
        IConfiguration configuration = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json", optional: true)
			.Build();
		var folderConfigurator = new FolderConfigurator(configuration);
		var project = Project.Create("2222", Stage.Create("pb","projekt budowlany"), "Name");
		var folder = $"{folderConfigurator.ActiveFolder}{project.Symbol}_{project.Name.Value}";
		if (Directory.Exists(folder))
		{
			Directory.Delete(folder, true);
		}
		var folderCommandCreator = new FolderCommandCreator(configuration, folderConfigurator, new Mock<ILogger<IFolderCommandCreator>>().Object);
		var manager = new FolderManager(
			folderConfigurator,
			folderCommandCreator,
			new Mock<ILogger<IFolderManager>>().Object);

		var (Success, _) = await manager.CreateProjectFolderAsync(project, CancellationToken.None);
		var dirExists = Directory.Exists(folder);

		Assert.True(Success);
		Assert.True(dirExists);

		if (dirExists)
		{
			Directory.Delete(folder, true);
		}
	}

	[Fact]
	internal async Task method_match_shuld_rename_exist_folder()
	{
		IConfiguration configuration = new ConfigurationBuilder()
		.AddJsonFile("appsettings.json", optional: true)
		.Build();
		var folderConfigurator = new FolderConfigurator(configuration);
		var project = Project.Create("3333", Stage.Create("pb", "projekt budowlany"), "Name");
		var folder = $"{folderConfigurator.ActiveFolder}{project.Symbol}_{project.Name.Value}";
		
		Directory.CreateDirectory($"{folderConfigurator.ActiveFolder}{project.Symbol}_NotMatchName");

		var folderCommandCreator = new FolderCommandCreator(configuration, folderConfigurator, new Mock<ILogger<IFolderCommandCreator>>().Object);
		var manager = new FolderManager(
			folderConfigurator,
			folderCommandCreator,
			new Mock<ILogger<IFolderManager>>().Object);

		var (Success, _) = await manager.MatchProjectFolderAsync(project, CancellationToken.None);
		var dirExists = Directory.Exists(folder);

		Assert.True(Success);
		Assert.True(dirExists);

		if (dirExists)
		{
			Directory.Delete(folder, true);
		}
	}

	[Fact]
	internal async Task method_match_shuld_create_new_folder()
	{
		IConfiguration configuration = new ConfigurationBuilder()
		.AddJsonFile("appsettings.json", optional: true)
		.Build();
		var folderConfigurator = new FolderConfigurator(configuration);
		var project = Project.Create("4444", Stage.Create("pb", "projekt budowlany"), "Name");
		var folder = $"{folderConfigurator.ActiveFolder}{project.Symbol}_{project.Name.Value}";

		var folderCommandCreator = new FolderCommandCreator(configuration, folderConfigurator, new Mock<ILogger<IFolderCommandCreator>>().Object);
		var manager = new FolderManager(
			folderConfigurator,
			folderCommandCreator,
			new Mock<ILogger<IFolderManager>>().Object);

		var (Success, _) = await manager.MatchProjectFolderAsync(project, CancellationToken.None);
		var dirExists = Directory.Exists(folder);

		Assert.True(Success);
		Assert.True(dirExists);

		if (dirExists)
		{
			Directory.Delete(folder, true);
		}
	}

	[Fact]
	internal async Task method_archive_shuld_move_not_empty_folder_to_archive()
	{
		IConfiguration configuration = new ConfigurationBuilder()
		.AddJsonFile("appsettings.json", optional: true)
		.Build();
		var folderConfigurator = new FolderConfigurator(configuration);
		var project = Project.Create("6666", Stage.Create("pb", "projekt budowlany"), "Name");
		var folderSource = $"{folderConfigurator.ActiveFolder}{project.Symbol}_{project.Name.Value}";
		Directory.CreateDirectory($"{folderConfigurator.ActiveFolder}{project.Symbol}_{project.Name.Value}");
		File.WriteAllText($"{folderConfigurator.ActiveFolder}{project.Symbol}_{project.Name.Value}\\Readme.txt", "");
		
		var folderCommandCreator = new FolderCommandCreator(configuration, folderConfigurator, new Mock<ILogger<IFolderCommandCreator>>().Object);
		var manager = new FolderManager(
			folderConfigurator,
			folderCommandCreator,
			new Mock<ILogger<IFolderManager>>().Object);

		var (Success, _) = await manager.ArchiveProjectFolderAsync(project, CancellationToken.None);
		var folderDest = $"{folderConfigurator.ArchiveFolder}{project.Symbol}_{project.Name.Value}";
		var destExist = Directory.Exists(folderDest);
		var sourceExist = Directory.Exists(folderSource);

		Assert.True(Success);
		Assert.True(destExist);
		Assert.False(sourceExist);

		if (sourceExist)
		{
			Directory.Delete(folderSource, true);
		}

		if (destExist)
		{
			Directory.Delete(folderDest, true);
		}
	}

	[Fact]
	internal async Task method_archive_shuld_return_false_when_folder_is_empty()
	{
		IConfiguration configuration = new ConfigurationBuilder()
		.AddJsonFile("appsettings.json", optional: true)
		.Build();
		var folderConfigurator = new FolderConfigurator(configuration);
		var project = Project.Create("7777", Stage.Create("pb", "projekt budowlany"), "Name");
		var folderSource = $"{folderConfigurator.ActiveFolder}{project.Symbol}_{project.Name.Value}";
		Directory.CreateDirectory($"{folderConfigurator.ActiveFolder}{project.Symbol}_{project.Name.Value}");

		var folderCommandCreator = new FolderCommandCreator(configuration, folderConfigurator, new Mock<ILogger<IFolderCommandCreator>>().Object);
		var manager = new FolderManager(
			folderConfigurator,
			folderCommandCreator,
			new Mock<ILogger<IFolderManager>>().Object);

		var (Success, _) = await manager.ArchiveProjectFolderAsync(project, CancellationToken.None);
		var folderDest = $"{folderConfigurator.ArchiveFolder}{project.Symbol}_{project.Name.Value}";
		var destExist = Directory.Exists(folderDest);
		var sourceExist = Directory.Exists(folderSource);

		Assert.False(Success);
		Assert.False(destExist);
		Assert.True(sourceExist);

		if (sourceExist)
		{
			Directory.Delete(folderSource, true);
		}

		if (destExist)
		{
			Directory.Delete(folderDest, true);
		}
	}

	[Fact]
	internal async Task method_restore_shuld_move_folder_to_actual()
	{
		IConfiguration configuration = new ConfigurationBuilder()
		.AddJsonFile("appsettings.json", optional: true)
		.Build();
		var folderConfigurator = new FolderConfigurator(configuration);
		var project = Project.Create("8888", Stage.Create("pb", "projekt budowlany"), "Name");
		var folderSource = $"{folderConfigurator.ArchiveFolder}{project.Symbol}_{project.Name.Value}";
		var folderDest = $"{folderConfigurator.ActiveFolder}{project.Symbol}_{project.Name.Value}";
		if (Directory.Exists(folderSource))
		{
			Directory.Delete(folderSource, true);
		}
		if (Directory.Exists(folderDest))
		{
			Directory.Delete(folderDest, true);
		}
		Directory.CreateDirectory($"{folderConfigurator.ArchiveFolder}{project.Symbol}_{project.Name}");
		File.WriteAllText($"{folderConfigurator.ArchiveFolder}{project.Symbol}_{project.Name}\\Readme.txt", "");


		var folderCommandCreator = new FolderCommandCreator(configuration, folderConfigurator, new Mock<ILogger<IFolderCommandCreator>>().Object);
		var manager = new FolderManager(
			folderConfigurator,
			folderCommandCreator,
			new Mock<ILogger<IFolderManager>>().Object);


		var (Success, _) = await manager.RestoreProjectFolderAsync(project, CancellationToken.None);


		Assert.True(Success);
		Assert.True(Directory.Exists(folderDest));
		Assert.False(Directory.Exists(folderSource));

		if (Directory.Exists(folderSource))
		{
			Directory.Delete(folderSource, true);
		}
		if (Directory.Exists(folderDest))
		{
			Directory.Delete(folderDest, true);
		}
	}


}
