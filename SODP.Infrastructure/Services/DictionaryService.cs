using AutoMapper;
using DocumentFormat.OpenXml.Wordprocessing;
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

        public Task<ServiceResponse> DeleteAsync(string master, string sign = "")
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<DictionaryDTO>> GetAsync(string master, string sign = "")
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<DictionaryDTO>> GetMasterAsync(string master, bool? active = null)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<DictionaryDTO>> GetSlaveAsync(string master, string sign)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> UpdateAsync(DictionaryDTO entity)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<DictionaryDTO>> CreateAsync(DictionaryDTO item)
        {
            var serviceResponse = new ServiceResponse<DictionaryDTO>();
            var newItem = _mapper.Map<AppDictionary>(item);
            try
            {
                if (await _context.AppDictionary.AnyAsync(x => x.Equals(newItem)))
                {
                    serviceResponse.SetError($"Error: Dictionary item {item.ParentId}:{item.Sign} exist.", 409);
                    return serviceResponse;
                }
                var entity = await _context.AppDictionary.AddAsync(newItem);
                await _context.SaveChangesAsync();
                serviceResponse.SetData(_mapper.Map<DictionaryDTO>(entity.Entity));
            }
            catch (Exception ex)
            {
                serviceResponse.SetError(ex.Message);
            }

            return serviceResponse;
        }

        //public async Task<ServicePageResponse<DictionaryDTO>> GetPageAsync(int? parent = null, bool? active = null, int currentPage = 1, int pageSize = 0, string searchString = "")
        //{
        //    ActiveFilter(active);
        //    _query = _query.Where(x => x.ParentId == parent);
        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        _query = _query.Where(x => x.Name.ToUpper().Contains(searchString.ToUpper()));
        //    }

        //    return await GetResponse(currentPage, pageSize);
        //}

        //public async Task<ServicePageResponse<DictionaryDTO>> GetPageAsync(string parent, bool? active, int currentPage, int pageSize, string searchString)
        //{
        //    ActiveFilter(active);
        //    _query = _query.Where(x => x.Parent.Sign == parent);

        //    return await GetResponse(currentPage,pageSize);
        //}

        public override AppService<AppDictionary, DictionaryDTO> SearchFilter(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                _query = _query.Where(x => x.Name.ToUpper().Contains(searchString.ToUpper()));
            }

            return this;
        }

        public AppService<AppDictionary, DictionaryDTO> Parent(string sign)
        {
            if (string.IsNullOrEmpty(sign))
            {
                _query = _query.Where(x => x.Parent == null);
            }
            else
            {
                _query = _query.Where(x => x.Parent != null && x.Parent.Sign.Equals(sign));
            }

            return this;
        }

        public AppService<AppDictionary, DictionaryDTO> Parent(int? id)
        {
            _query = _query.Where(x => x.ParentId == id);

            return this;
        }

        public IAppService GetActiveStatus(bool status)
        {
            throw new NotImplementedException();
        }

        //public async Task<ServiceResponse> DeleteAsync(string master, string sign = "")
        //{
        //    var serviceResponse = new ServiceResponse();
        //    try
        //    {
        //        IEnumerable<AppDictionary> collection = null;
        //        if (string.IsNullOrEmpty(sign))
        //        {
        //            collection = _context.AppDictionary.Where(x => x.Master.Equals(master));
        //            _context.AppDictionary.RemoveRange(collection);
        //            var item = _context.AppDictionary.FirstOrDefault(x => string.IsNullOrEmpty(x.Master) && x.Sign.Equals(master));
        //            if (item == null)
        //            {
        //                serviceResponse.SetError($"Error: Dictionary item {master}:{sign} not found", 404);
        //                return serviceResponse;
        //            }
        //            _context.AppDictionary.Remove(item);
        //        }
        //        else
        //        {
        //            var item = _context.AppDictionary.FirstOrDefault(x => x.Master.Equals(master) && x.Sign.Equals(sign));
        //            if (item == null)
        //            {
        //                serviceResponse.SetError($"Error: Dictionary item {master}:{sign} not found.", 404);
        //                return serviceResponse;
        //            }
        //        }
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        serviceResponse.SetError(ex.Message);
        //    }

        //    return serviceResponse;
        //}

        //public async Task<ServiceResponse> UpdateAsync(DictionaryDTO entity)
        //{
        //    var serviceResponse = new ServiceResponse();
        //    var item = await _context.AppDictionary.FirstOrDefaultAsync(x => x.Id == entity.Id);
        //    if (item == null)
        //    {
        //        serviceResponse.SetError($"Error: Entity {entity.Id} not found", 404);
        //        return serviceResponse;
        //    }
        //    item.Name = entity.Name;
        //    _context.Entry(item).State = EntityState.Modified;
        //    await _context.SaveChangesAsync();

        //    return serviceResponse;
        //}

        //public async Task<ServiceResponse<DictionaryDTO>> GetMasterAsync(string master, bool? active = null)
        //{
        //    ArgumentNullException.ThrowIfNull(master, nameof(master));
        //    var serviceResponse = new ServiceResponse<DictionaryDTO>();
        //    try
        //    {
        //        var item = await _context.AppDictionary.FirstOrDefaultAsync(x => string.IsNullOrEmpty(x.Master) && x.Sign.Equals(master));
        //        if (item == null)
        //        {
        //            serviceResponse.SetError($"Dictionary item {master} not found", 404);
        //            return serviceResponse;
        //        }
        //        item.Slaves = await _context.AppDictionary.Where(x => x.Master.Equals(master) && (active == null || x.ActiveStatus == active)).ToListAsync();
        //        serviceResponse.SetData(_mapper.Map<DictionaryDTO>(item));
        //    }
        //    catch (Exception ex)
        //    {
        //        serviceResponse.SetError(ex.Message);
        //    }

        //    return serviceResponse;
        //}

        //public async Task<ServiceResponse<DictionaryDTO>> GetSlaveAsync(string master, string sign)
        //{
        //    ArgumentNullException.ThrowIfNull(master, nameof(master));
        //    ArgumentNullException.ThrowIfNull(sign, nameof(sign));
        //    var serviceResponce = new ServiceResponse<DictionaryDTO>();

        //    try
        //    {
        //        var item = await _context.AppDictionary.FirstOrDefaultAsync(x => x.Master.Equals(master) && x.Sign.Equals(sign));
        //        if (item == null)
        //        {
        //            serviceResponce.SetError($"Error: Dictionary item {master}:{sign} not found", 404);
        //            return serviceResponce;
        //        }
        //        serviceResponce.SetData(_mapper.Map<DictionaryDTO>(item));
        //    }
        //    catch (Exception ex)
        //    {
        //        serviceResponce.SetError(ex.Message);
        //    }

        //    return serviceResponce;
        //}

        //public async Task<ServiceResponse<DictionaryDTO>> GetAsync(string master, string sign = "")
        //{
        //    ArgumentNullException.ThrowIfNull(master, nameof(master));
        //    var serviceResponse = new ServiceResponse<DictionaryDTO>();
        //    try
        //    {
        //        var item = await _context.AppDictionary
        //            .Include(x => x.Children)
        //            .FirstOrDefaultAsync(x => (sign == "" ? x.Sign == master : (x.Master == master && x.Sign == sign)));
        //        if (item == null)
        //        {
        //            serviceResponse.SetError($"Error: Dictionary item {master}:{sign} not found.", 404);
        //            return serviceResponse;
        //        }
        //        serviceResponse.SetData(_mapper.Map<DictionaryDTO>(item));
        //    }
        //    catch (Exception ex)
        //    {
        //        serviceResponse.SetError(ex.Message);
        //    }

        //    return serviceResponse;
        //}
    }
}
