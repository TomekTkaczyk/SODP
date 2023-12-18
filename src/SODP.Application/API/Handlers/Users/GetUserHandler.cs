using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Users;
using SODP.Domain.Attributes;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Users;

public class GetUserHandler : IRequestHandler<GetUserRequest, ApiResponse<UserDTO>>
{
	private readonly IUserRepository _userRepository;
	private readonly IMapper _mapper;

	public GetUserHandler(
		IUserRepository userRepository,
		IMapper mapper)
	{
		_userRepository = userRepository;
		_mapper = mapper;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse<UserDTO>> Handle(GetUserRequest request, CancellationToken cancellationToken)
	{
		var user = await _userRepository.Users
			.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken)
			?? throw new NotFoundException("User");

		return ApiResponse.Success(_mapper.Map<UserDTO>(user));
	}
}
