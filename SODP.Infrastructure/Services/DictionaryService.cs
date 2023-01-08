using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Services;
using SODP.DataAccess;
using SODP.Model;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Infrastructure.Services
{
	public class DictionaryService : AppService<AppDictionary, DictionaryDTO>, IDictionaryService
	{
		public DictionaryService(IMapper mapper, IValidator<AppDictionary> validator, SODPDBContext context, IActiveStatusService<AppDictionary> activeStatusService) : base(mapper, validator, context, activeStatusService) { }

		public async Task<ServiceResponse<DictionaryDTO>> GetAsync(string masterSign, string slaveSign = "")
		{
			ArgumentNullException.ThrowIfNull(masterSign, nameof(masterSign));
			var serviceResponse = new ServiceResponse<DictionaryDTO>();
			try
			{
				var item = await _context.AppDictionary
					.Include(x => x.Slaves)
					.FirstOrDefaultAsync(x => (slaveSign == "" ? x.Sign == masterSign : (x.Master == masterSign && x.Sign == slaveSign)));
				if (item == null)
				{
					serviceResponse.SetError($"Error: Dictionary item {masterSign}:{slaveSign} not found.", 401);
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

		public async Task<ServiceResponse<DictionaryDTO>> CreateAsync(DictionaryDTO item)
		{
			var serviceResponse = new ServiceResponse<DictionaryDTO>();
			try
			{
				var dictItem = await _context.AppDictionary
				   .Include(x => x.Slaves)
				   .FirstOrDefaultAsync(x => (item.Sign == "" ? x.Sign == item.Master : (x.Master == item.Master && x.Sign == item.Sign)));
				if (dictItem != null)
				{
					serviceResponse.SetError($"Error: Dictionary item {item.Master}:{item.Sign} exist.");
					return serviceResponse;
				}

				if (!string.IsNullOrEmpty(item.Master))
				{
					dictItem = await _context.AppDictionary.FirstOrDefaultAsync(x => x.Master == item.Master);
					if (dictItem == null)
					{
						serviceResponse.SetError($"Error: Dictionary item {item.Master} not exist.");
						return serviceResponse;
					}
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

        public async Task<ServicePageResponse<DictionaryDTO>> GetPageAsync(bool? active, int currentPage = 1, int pageSize = 0, string searchString = "")
        {
			return await GetPageAsync(null, active, currentPage, pageSize, searchString);
        }

        public async Task<ServicePageResponse<DictionaryDTO>> GetPageAsync(string masterSign, bool? active, int currentPage = 1, int pageSize = 0, string searchString = "")
        {
            SetActiveFilter(active);

            _query = _query.Where(x => active == null || x.ActiveStatus == active);

            if (string.IsNullOrEmpty(masterSign))
			{
				_query = _query.Where(x => string.IsNullOrEmpty(x.Master));
            }
            else
			{
                _query = _query.Where(x => x.Master.ToUpper().Equals(masterSign.ToUpper()));
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

        public Task<ServiceResponse<DictionaryDTO>> DeleteAsync(string masterSign, string slaveSign)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> UpdateAsync(DictionaryDTO entity)
        {
            throw new NotImplementedException();
        }
    }
}
