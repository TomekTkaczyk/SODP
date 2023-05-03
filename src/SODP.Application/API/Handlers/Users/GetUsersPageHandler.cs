using AutoMapper;
using MediatR;
using SODP.Application.API.Requests.Users;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Users
{
	public sealed class GetUsersPageHandler : IRequestHandler<GetUsersPageRequest, ApiResponse<Page<UserDTO>>>
	{
		private readonly IUserRepository _userRepository;
		private readonly IMapper _mapper;

		public GetUsersPageHandler(
			IUserRepository userRepository,
			IMapper _mapper)
		{
			_userRepository = userRepository;
			this._mapper = _mapper;
		}

		public async Task<ApiResponse<Page<UserDTO>>> Handle(GetUsersPageRequest request, CancellationToken cancellationToken)
		{
			var page = await _userRepository.GetPageAsync(
				request.ActiveStatus,
				request.SearchString,
				request.PageNumber,
				request.PageSize,
				cancellationToken);

			return ApiResponse.Success(_mapper.Map<Page<UserDTO>>(page));
		}
	}
}
