using MediatR;
using SODP.Shared.Response;

namespace SODP.Application.Abstractions;

public interface ICommandHandler<TCommand>
	: IRequestHandler<TCommand, ApiResponse> where TCommand : ICommand
{
}

public interface ICommandHandler<TCommand, TResponse>
	: IRequestHandler<TCommand, ApiResponse<TResponse>> where TCommand : ICommand<TResponse>
{
}
