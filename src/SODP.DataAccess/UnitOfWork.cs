using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.DataAccess
{
	public sealed class UnitOfWork : IUnitOfWork
	{
		private readonly SODPDBContext _dbContext;

		public UnitOfWork(SODPDBContext dbContext)
        {
			_dbContext = dbContext;
		}

		public Task SaveChangesAsync(CancellationToken cancellationToken = default)
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
				if(entityEntry.State == EntityState.Added)
				{
					entityEntry.Property(a => a.CreateTimeStamp).CurrentValue = DateTime.UtcNow;
				}
				if (entityEntry.State == EntityState.Modified)
				{
					entityEntry.Property(a => a.ModifyTimeStamp).CurrentValue = DateTime.UtcNow;
				}
			}
		}

		#endregion
	}
}
