using MediatR;
using SODP.Shared.Response;

namespace SODP.Application.Abstractions;

public interface ICommand : IRequest<ApiResponse> { }

public interface ICommand<TResponse> : IRequest<ApiResponse<TResponse>> { }
