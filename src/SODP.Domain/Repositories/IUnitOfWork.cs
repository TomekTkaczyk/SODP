﻿using System.Threading;
using System.Threading.Tasks;

namespace SODP.Domain.Repositories
{
	public interface IUnitOfWork
	{
		Task SaveChangesAsync(CancellationToken cancellationToken);
	}
}