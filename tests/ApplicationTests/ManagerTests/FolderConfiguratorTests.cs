using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using SODP.Application.Abstractions;
using SODP.Infrastructure.Managers;
using SODP.Shared.Enums;
using Xunit;

namespace tests.ApplicationTests.ManagerTests;

public class FolderConfiguratorTests
{
	[Fact]
	internal void FolderCommanCreator_shold_return_valid_commands()
	{
		IConfiguration configuration = new ConfigurationBuilder()
			.AddJsonFile("appsettings.json", optional: true)
			.Build();
		var logger = new Mock<ILogger<IFolderCommandCreator>>();
		IFolderConfigurator configurator = new FolderConfigurator(configuration);

		var commandCreator = new FolderCommandCreator(configuration,configurator,logger.Object);

		var command = commandCreator.GetCommandCreateFolder(ProjectsFolder.Active, "1111pb_test").ToUpper();
		Assert.Equal("/C F:\\HOME\\SODP\\CREATEPROJECT.CMD F:\\HOME\\SODP\\AKTUALNE\\ 1111PB_TEST", command);

		command = commandCreator.GetCommandRenameFolder(ProjectsFolder.Active, "1111pb_test", "1111pb_newTest").ToUpper();
		Assert.Equal("/C F:\\HOME\\SODP\\RENAMEPROJECT.CMD F:\\HOME\\SODP\\AKTUALNE\\ 1111PB_TEST 1111PB_NEWTEST", command);

		command = commandCreator.GetCommandDeleteFolder(ProjectsFolder.Active, "1111pb_test").ToUpper();
		Assert.Equal("/C F:\\HOME\\SODP\\DELETEPROJECT.CMD F:\\HOME\\SODP\\AKTUALNE\\ 1111PB_TEST", command);

		command = commandCreator.GetCommandArchiveFolder("1111pb_test").ToUpper();
		Assert.Equal("/C F:\\HOME\\SODP\\ARCHIVEPROJECT.CMD F:\\HOME\\SODP\\AKTUALNE\\ F:\\HOME\\SODP\\ZAKONCZONE\\ 1111PB_TEST", command);

		command = commandCreator.GetCommandRestoreFolder("1111pb_test").ToUpper();
		Assert.Equal("/C F:\\HOME\\SODP\\RESTOREPROJECT.CMD F:\\HOME\\SODP\\ZAKONCZONE\\ F:\\HOME\\SODP\\AKTUALNE\\ 1111PB_TEST", command);

	}
}
