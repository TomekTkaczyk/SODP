using Microsoft.Extensions.Logging;
using Moq;
using SODP.Application.Specifications.Stages;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Infrastructure.Repositories;
using System.Linq;
using tests.Utils;
using Xunit;

namespace tests.SpecificationTests;

public class StageByNameSpecificationTests
{
	private readonly SODPDBContext _context;
	private readonly StageRepository _stageRepository;

    public StageByNameSpecificationTests()
    {
		var mock = new Mock<ILogger<Stage>>();

		_context = SODPDbContextFactory.CreateDbContext("Server=localhost;Database=SODP;Uid=sodpdbuser;Pwd=sodpdbpassword;");
		_stageRepository = new StageRepository(_context, mock.Object);
	}

	[Fact]
	internal void repository_GetAll_shuld_return_one_stage_PWKS()
	{
		var specification = new StagesSearchSpecification(null, "PWKS");
		var stage = _stageRepository.Get(specification).FirstOrDefault();

		Assert.NotNull(stage);
		Assert.Equal("PWKS", stage.Sign.Value);
	}

	[Fact]
	internal void repository_GetAll_should_return_the_full_sorted_list()
	{
		var specification = new StagesSearchSpecification();

		var stage = _stageRepository.Get(specification).FirstOrDefault();

		Assert.NotNull(stage);
		Assert.True(stage.Sign == "IO");
	}

	[Fact]
	internal void repository_GetAll_should_return_the_sorted_list_content_OPINIA()
	{
		var specification = new StagesSearchSpecification(null,"opinia");

		var stage = _stageRepository.Get(specification).FirstOrDefault();

		Assert.NotNull(stage);
		Assert.True(stage.Sign == "OT");
	}
}
