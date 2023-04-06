using MediatR;
using SODP.Shared.Response;

namespace SODP.Application.Abstractions;

public interface ICommandHandler<in TCommand>
	: IRequestHandler<TCommand, ApiResponse> where TCommand : ICommand
{
}

public interface ICommandHandler<in TCommand, TResponse>
	: IRequestHandler<TCommand, ApiResponse<TResponse>> where TCommand : ICommand<TResponse>
{
}
