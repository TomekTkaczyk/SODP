using AutoMapper;
using FluentValidation;
using SODP.Application.Services;
using SODP.DataAccess;
using SODP.Model;
using SODP.Model.Interfaces;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Infrastructure.Services
{
    public abstract class FilteredPageService<TEntity, TDto> : AppService<TEntity, TDto> where TEntity : BaseEntity where TDto : BaseDTO
    {
        protected readonly IActiveStatusService<TEntity> _activeStatusService;

        public FilteredPageService(IMapper mapper, IValidator<TEntity> validator, SODPDBContext context, IActiveStatusService<TEntity> activeStatusService) : base(mapper, validator, context) 
        {
            _activeStatusService = activeStatusService;
        }

        public async Task<ServicePageResponse<TDto>> GetPageAsync(bool? active, int currentPage = 1, int pageSize = 0, string searchString = "")
        {
            if (active.HasValue)
            {
                WithActiveStatus(active.Value);
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                WithSearchString(searchString);
            }

            return await GetPageAsync(currentPage, pageSize);
        }

        private FilteredPageService<TEntity, TDto> WithActiveStatus(bool active)
        {
            if (typeof(IActiveStatus).IsAssignableFrom(typeof(TEntity)))
            {
                _query = _query.Where(x => ((IActiveStatus)x).ActiveStatus == active);
            }
            _totalCount = _query.Count();

            return this;
        }

        public async Task<ServiceResponse> SetActiveStatusAsync(int id, bool status)
        {
            return await _activeStatusService.SetActiveStatusAsync(id, status);
        }

        protected abstract FilteredPageService<TEntity, TDto> WithSearchString(string searchString);
    }
}
