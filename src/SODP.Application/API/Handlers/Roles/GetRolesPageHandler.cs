using AutoMapper;
using MediatR;
using SODP.Application.API.Requests.Roles;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Roles
{
	public sealed class GetRolesPageHandler : IRequestHandler<GetRolesPageRequest, ApiResponse<Page<RoleDTO>>>
	{
		private readonly IRoleRepository _roleRepository;
		private readonly IMapper _mapper;

		public GetRolesPageHandler(
			IRoleRepository roleRepository,
			IMapper _mapper)
		{
			_roleRepository = roleRepository;
			this._mapper = _mapper;
		}

		public async Task<ApiResponse<Page<RoleDTO>>> Handle(GetRolesPageRequest request, CancellationToken cancellationToken)
		{
			var page = await _roleRepository.GetPageAsync(
				request.ActiveStatus,
				request.PageNumber,
				request.PageSize,
				cancellationToken);

			return ApiResponse.Success(_mapper.Map<Page<RoleDTO>>(page));
		}
	}
}
