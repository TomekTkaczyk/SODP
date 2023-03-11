using AutoMapper;
using FluentValidation;
using SODP.Application.Services;
using SODP.DataAccess;
using SODP.Model;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Infrastructure.Services
{
    public abstract class FilteredPageService<TEntity, TDto> : AppService<TEntity, TDto> where TEntity : BaseEntity, new() where TDto : BaseDTO
    {
        protected readonly IActiveStatusService<TEntity> _activeStatusService;

        public FilteredPageService(IMapper mapper, IValidator<TEntity> validator, SODPDBContext context, IActiveStatusService<TEntity> activeStatusService) : base(mapper, validator, context) 
        {
            _activeStatusService = activeStatusService;
        }

        public async Task<ServiceResponse> SetActiveStatusAsync(int id, bool status)
        {
            return await _activeStatusService.SetActiveStatusAsync(id, status);
        }

        public abstract Task<ServicePageResponse<TDto>> GetPageAsync(bool? active, string searchString, int currentPage = 1, int pageSize = 0);

    }
}
