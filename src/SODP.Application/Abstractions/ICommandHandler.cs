using MediatR;
using SODP.Domain.Shared;

namespace SODP.Application.Abstractions
{
	public interface ICommandHandler<TCommand>
		: IRequestHandler<TCommand, Result> where TCommand : ICommand
	{
	}

	public interface ICommandHandler<TCommand, TResponse>
		: IRequestHandler<TCommand, Result<TResponse>> where TCommand : ICommand<TResponse>
	{
	}
}
