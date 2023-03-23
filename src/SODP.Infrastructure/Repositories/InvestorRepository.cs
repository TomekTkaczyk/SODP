using Microsoft.EntityFrameworkCore;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Infrastructure.Specyfications;

namespace SODP.Infrastructure.Repositories
{
	internal class InvestorRepository : IInvestorRepository
	{
		private readonly SODPDBContext _dbContext;

		public InvestorRepository(SODPDBContext dbContext)
        {
			_dbContext = dbContext;
		}

		public async Task<Investor> Create(Investor investor, CancellationToken cancellationToken = default)
		{
			var entity = _dbContext.Set<Investor>().FirstOrDefaultAsync(x => x.Name.Equals(investor.Name), cancellationToken);
			if (entity != null)
			{
				throw new InvestorExistException();
			}

			var entry = await _dbContext.Set<Investor>().AddAsync(investor, cancellationToken);

			return entry.Entity;
		}

		public async Task Remove(Investor investor, CancellationToken cancellationToken = default)
		{
			var entity = await _dbContext.Set<Investor>().FirstOrDefaultAsync(x => x.Id == investor.Id, cancellationToken);
			if (entity == null)
			{
				throw new InvestorNotFoundException();
			}

			_dbContext.Set<Investor>().Remove(entity);
		}

		public async Task Update(Investor investor, CancellationToken cancellationToken)
		{
			var entity = await _dbContext.Set<Investor>().FirstOrDefaultAsync(x => x.Id == investor.Id, cancellationToken);
			if (entity == null)
			{
				throw new InvestorNotFoundException();
			}

			_dbContext.Set<Investor>().Update(entity);
		}

		public async Task SetActiveStatusAsync(int id, bool status, CancellationToken cancellationToken = default)
		{
			var entity = await _dbContext.Set<Investor>().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);

			entity.ActiveStatus = status;
			_dbContext.Entry(entity).State = EntityState.Modified;
		}


		public async Task<Investor> GetById(int id, CancellationToken cancellationToken)
		{
			var entity = await ApplySpecyfication(new InvestorByIdSpecyfication(id))
				.SingleOrDefaultAsync(cancellationToken);

			if(entity == null)
			{
				throw new InvestorNotFoundException();
			}

			return entity;
		}

		public async Task<ICollection<Investor>> GetPageAsync(bool? active, string searchString, int currentPage, int pageSize, CancellationToken cancellationToken = default)
		{
			var queryable = ApplySpecyfication(new InvestorByNameSpecyfication(active, searchString));
			if (pageSize > 0)
			{
				queryable = queryable.Skip((currentPage - 1) * pageSize).Take(pageSize);
			}

			return await queryable.ToListAsync(cancellationToken);
		}


		private IQueryable<Investor> ApplySpecyfication(Specification<Investor> specification)
		{
			return SpecificationEvaluator.GetQuery(_dbContext.Set<Investor>(),specification);
		}
	}
}
