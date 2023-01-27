using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
	public abstract class AppService<TEntity,TDto> where TEntity : BaseEntity, new() where TDto : BaseDTO
    {
		protected IQueryable<TEntity> _query;
		protected int _totalCount;

		protected readonly IMapper _mapper;
		protected readonly IValidator<TEntity> _validator;
		protected readonly SODPDBContext _context;

		public AppService(IMapper mapper, IValidator<TEntity> validator, SODPDBContext context)
        {
			_mapper = mapper;
			_validator = validator;
			_context = context;

			_query = _context.Set<TEntity>().AsQueryable();
			_totalCount = _query.Count();
		}

        protected IQueryable<TEntity> PageQuery(int currentPage, int pageSize)
        {

			if (currentPage < 1)
			{
				throw new ArgumentOutOfRangeException(nameof(currentPage),"Bad argument. Required currentPage > 0");
			}

            if (pageSize > 0)
            {
                _query = _query.Skip((currentPage - 1) * pageSize).Take(pageSize);
            }

            return _query;
        }

        public virtual async Task<ServiceResponse<TDto>> GetAsync(int id)
		{
			var serviceResponse = new ServiceResponse<TDto>();
			try
			{
				var entity = await _query.FirstOrDefaultAsync(x => x.Id == id);
				if (entity == null)
				{
					serviceResponse.SetError($"Error: Entity Id:{id} not found.", 404);
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

		public virtual async Task<ServicePageResponse<TDto>> GetPageAsync(int currentPage = 1, int pageSize = 0)
		{
			var serviceResponse = new ServicePageResponse<TDto>();
			try
			{
				serviceResponse.Data.TotalCount = await _query.CountAsync();
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

		public virtual async Task<ServiceResponse> DeleteAsync(int id)
		{
			var serviceResponse = new ServiceResponse();

			try
			{
				var entity = new TEntity() { Id = id };
				_context.Entry(entity).State = EntityState.Deleted;

				if (await _context.SaveChangesAsync() == 0)
				{
					serviceResponse.SetError($"Error: Entity Id:{id} not found.", 404);
				}
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

    }
}
