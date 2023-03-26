using Microsoft.EntityFrameworkCore;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
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


		public void SetActiveStatus(int id, bool status)
		{
			throw new NotImplementedException();
		}

		public void SetActiveStatus(Investor investor, bool status)
		{
			var entry = _dbContext.Entry(investor);
			entry.Entity.ActiveStatus = status;
			_dbContext.Entry(investor).State = EntityState.Modified;
		}


		public async Task<Investor> GetByIdAsync(int id, CancellationToken cancellationToken)
		{
			var specyfication = new InvestorByIdSpecification(id);
			var entity = await ApplySpecyfication(specyfication).SingleOrDefaultAsync(cancellationToken);

			return entity ?? throw new InvestorNotFoundException();
		}

		public async Task<Investor> GetByNameAsync(string name, CancellationToken cancellationToken)
		{
			var specyfication = new InvestorByNameSpecification(null, name);
			var entity = await ApplySpecyfication(specyfication).FirstOrDefaultAsync(cancellationToken);

			return entity ?? throw new InvestorNotFoundException();
		}

		public async Task<ICollection<Investor>> GetPageAsync(bool? active, string searchString, int currentPage, int pageSize, CancellationToken cancellationToken = default)
		{
			var specification = new InvestorByNameSpecification(active, searchString);
			var queryable = ApplySpecyfication(specification);
			if (pageSize > 0)
			{
				queryable = queryable.Skip((currentPage - 1) * pageSize).Take(pageSize);
			}

			return await queryable.ToListAsync(cancellationToken);
		}
	}
}
