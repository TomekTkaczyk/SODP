using MediatR;
using SODP.Shared.Response;

namespace SODP.Application.Abstractions;

public interface IQuery<TResponse> : IRequest<ApiResponse<TResponse>> { }
