using Microsoft.Extensions.Logging;
using Moq;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Infrastructure.Repositories;
using System.Linq;
using System.Linq.Dynamic.Core;
using tests.Utils;
using Xunit;

namespace tests.RepositoryTests;

public class StageRepositoryTests
{
    private readonly SODPDBContext _context;

    public StageRepositoryTests()
    {
        _context = SODPDbContextFactory.CreateDbContext("Server=localhost;Database=SODP;Uid=sodpdbuser;Pwd=sodpdbpassword;");
	}

	[Fact]
	internal void repository_GetAll_should_return_entities()
	{
		var mock = new Mock<ILogger<Stage>>();
		var repository = new StageRepository(_context, mock.Object);

		var stages = repository.Get();

		Assert.NotNull(stages);
		Assert.True(stages.Any());
	}

	[Fact]
	internal void repository_GetAll_with_where_clausure_should_return_one_entity()
	{
		var mock = new Mock<ILogger<Stage>>();
		var repository = new StageRepository(_context, mock.Object);

		var stage = repository.Get().Where(x => x.Sign == "PB").FirstOrDefault();

		Assert.NotNull(stage);
	}
}
