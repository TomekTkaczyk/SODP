﻿using Microsoft.EntityFrameworkCore;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using SODP.Shared.Response;

namespace SODP.Infrastructure.Repositories;

public sealed class StageRepository : PagedRepository<Stage>, IStageRepository
{
	public StageRepository(SODPDBContext dbContext) : base(dbContext) { }

	public async Task<Stage> GetBySignAsync(string sign, CancellationToken cancellationToken)
	{
		return await _dbContext.Set<Stage>().SingleOrDefaultAsync(x => x.Sign.Equals(sign), cancellationToken);
	}

	//public async Task<Stage> GetByIdAsync(int id, CancellationToken cancellationToken)
	//{
	//	return await _dbContext.Set<Stage>()
	//		.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
	//}

	//public async Task<Stage> GetBySignAsync(string sign, CancellationToken cancellationToken)
	//{
	//	return await _dbContext.Set<Stage>()
	//		.SingleOrDefaultAsync(x => x.Sign == sign, cancellationToken);
	//}

	//public async Task<Page<Stage>> GetPageAsync(bool? active, string searchString, int pageNumber, int pageSize, CancellationToken cancellationToken)
	//{
	//	var specyfication = new StageByNameSpecification(active, searchString);
	//	var queryable = ApplySpecyfication(specyfication);
	//	var totalItems = await queryable.CountAsync(cancellationToken);

	//	if (pageSize > 0)
	//	{
	//		queryable = GetPageQuery(queryable, pageNumber, pageSize);
	//	}

	//	var collection = await queryable.ToListAsync(cancellationToken);

	//	return Page<Stage>.Create(
	//		collection,
	//		pageNumber,
	//		pageSize,
	//		totalItems);
	//}
}