using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Services;
using SODP.DataAccess;
using SODP.Model;
using SODP.Model.Interfaces;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SODP.Infrastructure.Services
{
	public class AppService<TEntity,TDto> where TEntity : BaseEntity where TDto : BaseDTO
    {
		private IQueryable<TEntity> query;

		protected readonly IMapper _mapper;
		protected readonly IValidator<TEntity> _validator;
		protected readonly SODPDBContext _context;
		protected readonly IActiveStatusService<TEntity> _activeStatusService;

		public AppService(IMapper mapper, IValidator<TEntity> validator, SODPDBContext context, IActiveStatusService<TEntity> activeStatusService)
        {
			_mapper = mapper;
			_validator = validator;
			_context = context;
			_activeStatusService = activeStatusService;

			query = _context.Set<TEntity>().AsQueryable();
		}


		public IQueryable<TEntity> GetQuery()
		{
			return query;
		}


		public virtual async Task<ServiceResponse<TDto>> GetAsync(int id)
		{
			var serviceResponse = new ServiceResponse<TDto>();
			try
			{
				var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
				if (entity == null)
				{
					serviceResponse.SetError($"Błąd: Encja Id:{id} nie odnaleziona.", 401);
					return serviceResponse;
				}
				serviceResponse.SetData(_mapper.Map<TDto>(entity));
			}
			catch (Exception ex)
			{
				serviceResponse.SetError(ex.Message);
			}

			return serviceResponse;
		}


		protected AppService<TEntity, TDto> SetActiveFilter(bool? active)
		{
			if (typeof(IActiveStatus).IsAssignableFrom(typeof(TEntity)))
			{
				query = query.Where(x => (active == null) || ((IActiveStatus)x).ActiveStatus == active);
			}

			return this;
		}


		protected IQueryable<TEntity> PageQuery(int currentPage = 1, int pageSize = 0)
		{
			if (pageSize > 0)
			{
				query = query.Skip((currentPage - 1) * pageSize).Take(pageSize);
			}

			return query;
		}


		public virtual async Task<ServicePageResponse<TDto>> GetPageAsync(bool? active, int currentPage = 1, int pageSize = 0)
		{
			SetActiveFilter(active);
			
			var serviceResponse = new ServicePageResponse<TDto>();
			try
			{
				serviceResponse.Data.TotalCount = await query.CountAsync();
				serviceResponse.Data.PageNumber= currentPage;
				serviceResponse.Data.PageSize= pageSize;
				serviceResponse.SetData(_mapper.Map<IList<TDto>>(await PageQuery(currentPage, pageSize).ToListAsync()));
			}
			catch (Exception ex)
			{
				serviceResponse.SetError(ex.Message);
			}

			return serviceResponse;
		}


		public async Task<bool> ExistAsync(int id)
		{
			return (await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id) != null);
		}


		public async Task<ServiceResponse> SetActiveStatusAsync(int id, bool status)
		{
			return await _activeStatusService.SetActiveStatusAsync(id, status);
		}


		public virtual async Task<ServiceResponse> DeleteAsync(int id)
		{
			var serviceResponse = new ServiceResponse();

			try
			{
				var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
				if (entity == null)
				{
					serviceResponse.SetError($"Błąd: Encja Id:{id} nie odnaleziona.", 401);
					return serviceResponse;
				}

				_context.Set<TEntity>().Remove(entity);
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
