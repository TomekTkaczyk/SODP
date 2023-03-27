using Microsoft.EntityFrameworkCore;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using SODP.Domain.ValueObjects;
using SODP.Infrastructure.Specifications.Investors;

namespace SODP.Infrastructure.Repositories
{
	internal class InvestorRepository : PagedRepository<Investor>, IInvestorRepository
	{
		public InvestorRepository(SODPDBContext dbContext) : base(dbContext) { }

		public Investor Add(Investor investor)
		{
			var entry = _dbContext.Set<Investor>().Add(investor);

			return entry.Entity;
		}

		public void Remove(Investor investor)
		{
			_dbContext.Entry(investor).State = EntityState.Deleted;
		}

		public void Update(Investor investor)
		{
			_dbContext.Set<Investor>().Update(investor);
			_dbContext.Entry(investor).State = EntityState.Modified;
		}

		public async Task<Investor> GetByIdAsync(int id, CancellationToken cancellationToken)
		{
			var specyfication = new InvestorByIdSpecification(id);

			//return await ApplySpecyfication(specyfication).SingleOrDefaultAsync(cancellationToken);

			return await _dbContext.Set<Investor>().SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
		}

		public async Task<Investor> GetByNameAsync(string name, CancellationToken cancellationToken)
		{
			//var specyfication = new InvestorByNameSpecification(null, name);
			//var entity = await ApplySpecyfication(specyfication).FirstOrDefaultAsync(cancellationToken);

			return await _dbContext.Set<Investor>().FirstOrDefaultAsync(x => x.Name.Equals(name), cancellationToken);
		}

		public async Task<Page<Investor>> GetPageAsync(bool? active, string searchString, int pageNumber, int pageSize, CancellationToken cancellationToken = default)
		{
			var specification = new InvestorByNameSpecification(active, searchString);
			var queryable = ApplySpecyfication(specification);
			var totalItems = await queryable.CountAsync(cancellationToken);

			if (pageSize > 0)
			{
				queryable = queryable.Skip((pageNumber - 1) * pageSize).Take(pageSize);
			}

			var collection = await queryable.ToListAsync(cancellationToken);

			return new Page<Investor>(
				collection,
				pageNumber,
				pageSize,
				totalItems);
		}
	}
}
