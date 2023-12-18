using AutoMapper;
using MediatR;
using SODP.Application.API.Requests.Users;
using SODP.Domain.Attributes;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Users;

public sealed class CreateUserHandler : IRequestHandler<CreateUserRequest, ApiResponse<UserDTO>>
{
	private readonly IUserRepository _userRepository;
	private readonly IMapper _mapper;

	public CreateUserHandler(
		IUserRepository userRepository,
		IMapper mapper)
	{
		_userRepository = userRepository;
		_mapper = mapper;
	}

	[IgnoreMethodAsyncNameConvention]
	public Task<ApiResponse<UserDTO>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
