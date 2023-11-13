using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;

namespace SODP.Infrastructure.Repositories;

public sealed class UnitOfWork : IUnitOfWork
{
	private readonly SODPDBContext _dbContext;

	public UnitOfWork(SODPDBContext dbContext)
	{
		_dbContext = dbContext;
	}

	public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
	{
		UpdateAuditableEntities();

		return _dbContext.SaveChangesAsync(cancellationToken);
	}

	#region Private methods

	private void UpdateAuditableEntities()
	{
		IEnumerable<EntityEntry<BaseEntity>> entities = _dbContext.ChangeTracker.Entries<BaseEntity>();
		foreach (EntityEntry<BaseEntity> entityEntry in entities)
		{
			if (entityEntry.State == EntityState.Added)
			{
				var utcNow = DateTime.UtcNow;
				entityEntry.Property(a => a.CreateTimeStamp).CurrentValue = utcNow;
				entityEntry.Property(a => a.ModifyTimeStamp).CurrentValue = utcNow;
				entityEntry.Property(a => a.CreatedById).CurrentValue = 1;
				entityEntry.Property(a => a.ModifiedById).CurrentValue = 1;
			}
			if (entityEntry.State == EntityState.Modified)
			{
				entityEntry.Property(a => a.ModifyTimeStamp).CurrentValue = DateTime.UtcNow;
				entityEntry.Property(a => a.ModifiedById).CurrentValue = 1;
			}
		}
	}

	#endregion
}
