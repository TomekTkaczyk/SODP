using Microsoft.EntityFrameworkCore;
using SODP.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.DataAccess.CQRS.Commands.Common;

public class UpdateCommand<TEntity> : CommandBase<TEntity> where TEntity : BaseEntity
{
	private readonly TEntity _entity;

	public UpdateCommand(TEntity entity)
    {
		_entity = entity;
	}

    public override async Task ExecuteAsync(SODPDBContext context, CancellationToken cancellationToken)
	{							   
		context.Entry(_entity).State = EntityState.Modified;
		await context.SaveChangesAsync(cancellationToken);
	}
}
