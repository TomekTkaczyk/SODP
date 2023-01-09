using AutoMapper;
using DocumentFormat.OpenXml.EMMA;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Services;
using SODP.DataAccess;
using SODP.Model;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Linq;
using System.Runtime.InteropServices;

namespace SODP.Infrastructure.Services
{
	public class DictionaryService : AppService<AppDictionary, DictionaryDTO>, IDictionaryService
	{
		public DictionaryService(IMapper mapper, IValidator<AppDictionary> validator, SODPDBContext context, IActiveStatusService<AppDictionary> activeStatusService) : base(mapper, validator, context, activeStatusService) { }

        public async Task<ServiceResponse<DictionaryDTO>> CreateAsync(DictionaryDTO item)
		{
			var serviceResponse = new ServiceResponse<DictionaryDTO>();
			try
			{
				var dictItem = await _context.AppDictionary
				   .Include(x => x.Slaves)
				   .FirstOrDefaultAsync(x => x.Master.Equals(item.Master) && x.Sign.Equals(item.Sign));
				if (dictItem != null)
				{
					serviceResponse.SetError($"Error: Dictionary item {item.Master}:{item.Sign} exist.",409);
					return serviceResponse;
				}

				dictItem = _mapper.Map<AppDictionary>(item);
				var entity = await _context.AppDictionary.AddAsync(dictItem);
				await _context.SaveChangesAsync();
				serviceResponse.SetData(_mapper.Map<DictionaryDTO>(entity.Entity));
			}
			catch (Exception ex)
			{
				serviceResponse.SetError(ex.Message);
			}

			return serviceResponse;
		}

        public async Task<ServicePageResponse<DictionaryDTO>> GetPageAsync(bool? active = null, int currentPage = 1, int pageSize = 0, string searchString = "")
        {
			return await GetPageAsync(null, active, currentPage, pageSize, searchString);
        }

        public async Task<ServicePageResponse<DictionaryDTO>> GetPageAsync(string master, bool? active = null, int currentPage = 1, int pageSize = 0, string searchString = "")
        {
            SetActiveFilter(active);

            _query = _query.Where(x => active == null || x.ActiveStatus == active);

            if (string.IsNullOrEmpty(master))
			{
				_query = _query.Where(x => string.IsNullOrEmpty(x.Master));
            }
            else
			{
                _query = _query.Where(x => x.Master.ToUpper().Equals(master.ToUpper()));
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                _query = _query.Where(x => x.Name.ToUpper().Contains(searchString.ToUpper()));
            }

            var serviceResponse = new ServicePageResponse<DictionaryDTO>();
            try
            {
                serviceResponse.Data.TotalCount = await _context.Set<AppDictionary>().CountAsync();
                serviceResponse.Data.PageNumber = currentPage;
                serviceResponse.Data.PageSize = pageSize;
                serviceResponse.SetData(_mapper.Map<IList<DictionaryDTO>>(await PageQuery(currentPage, pageSize).ToListAsync()));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse> DeleteAsync(string master, string sign = "")
        {
			var serviceResponse = new ServiceResponse();
			try
			{
				IEnumerable<AppDictionary> collection = null;
				if (string.IsNullOrEmpty(sign))
				{
					collection = _context.AppDictionary.Where(x => x.Master.Equals(master));
                    _context.AppDictionary.RemoveRange(collection);
					var item = _context.AppDictionary.FirstOrDefault(x => string.IsNullOrEmpty(x.Master) && x.Sign.Equals(master));
                    if (item == null)
                    {
                        serviceResponse.SetError($"Error: Dictionary item {master}:{sign} not found", 404);
                        return serviceResponse;
                    }
                    _context.AppDictionary.Remove(item);
                }
				else
				{
					var item = _context.AppDictionary.FirstOrDefault(x => x.Master.Equals(master) && x.Sign.Equals(sign));
					if(item == null)
					{
						serviceResponse.SetError($"Error: Dictionary item {master}:{sign} not found.",404);
						return serviceResponse;
					}
				}
				await _context.SaveChangesAsync();
            }
            catch (Exception ex)
			{
                serviceResponse.SetError(ex.Message);
            }

			return serviceResponse;
        }

        public async Task<ServiceResponse> UpdateAsync(DictionaryDTO entity)
        {
            var serviceResponse = new ServiceResponse();
            var item = await _context.AppDictionary.FirstOrDefaultAsync(x => x.Id == entity.Id);
			if (item == null)
			{
				serviceResponse.SetError($"Error: Entity {entity.Id} not found", 404);
				return serviceResponse;
			}
			item.Name = entity.Name;
			_context.Entry(item).State = EntityState.Modified;
			await _context.SaveChangesAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<DictionaryDTO>> GetMasterAsync(string master, bool? active = null)
        {
            ArgumentNullException.ThrowIfNull(master, nameof(master));
            var serviceResponse = new ServiceResponse<DictionaryDTO>();
            try
            {
                var item = await _context.AppDictionary.FirstOrDefaultAsync(x => string.IsNullOrEmpty(x.Master) && x.Sign.Equals(master));
                if (item == null)
                {
                    serviceResponse.SetError($"", 404);
                    return serviceResponse;
                }
                item.Slaves = await _context.AppDictionary.Where(x => x.Master.Equals(master) && (active == null || x.ActiveStatus == active)).ToListAsync();
                serviceResponse.SetData(_mapper.Map<DictionaryDTO>(item));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message);
                return serviceResponse;
            }

            return serviceResponse;
        }

        public Task<ServiceResponse<DictionaryDTO>> GetSlaveAsync(string master, string sign)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<DictionaryDTO>> GetAsync(string master, string sign = "")
        {
            ArgumentNullException.ThrowIfNull(master, nameof(master));
            var serviceResponse = new ServiceResponse<DictionaryDTO>();
            try
            {
                var item = await _context.AppDictionary
                    .Include(x => x.Slaves)
                    .FirstOrDefaultAsync(x => (sign == "" ? x.Sign == master : (x.Master == master && x.Sign == sign)));
                if (item == null)
                {
                    serviceResponse.SetError($"Error: Dictionary item {master}:{sign} not found.", 404);
                    return serviceResponse;
                }
                serviceResponse.SetData(_mapper.Map<DictionaryDTO>(item));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message);
            }

            return serviceResponse;
        }

    }
}
