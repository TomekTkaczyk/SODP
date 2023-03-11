using Microsoft.EntityFrameworkCore;
using SODP.Application.Services;
using SODP.DataAccess;
using SODP.Model;
using SODP.Model.Interfaces;
using SODP.Shared.Response;

namespace SODP.Infrastructure.Services
{
	public class ActiveStatusService<TEntity> : IActiveStatusService<TEntity> where TEntity : BaseEntity
	{
		private readonly SODPDBContext _context;

		public ActiveStatusService(SODPDBContext context)
		{
			_context = context;
		}


		public async Task<ServiceResponse> SetActiveStatusAsync(int id, bool status)
		{
			var serviceResponse = new ServiceResponse();
			try
			{
				var dbset = _context.Set<TEntity>();
				var entity = await dbset.SingleOrDefaultAsync(x => x.Id == id);

				if (entity == null)
				{
					serviceResponse.SetError($"Entity Id:{id} not exist.", 404);

					return serviceResponse;
				}

				if(entity is not IActiveStatus)
				{
					serviceResponse.SetError($"Entity Id:{id} property ActiveStatus not exist.", 404);

					return serviceResponse;
				}

				((IActiveStatus)entity).ActiveStatus = status;
				dbset.Update(entity);
				await _context.SaveChangesAsync();
			}
			catch (Exception ex)
			{
				serviceResponse.SetError(ex.Message);
			}

			return serviceResponse;
		}
	}
}
