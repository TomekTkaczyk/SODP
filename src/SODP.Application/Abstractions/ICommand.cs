using MediatR;
using SODP.Domain.Shared;

namespace SODP.Application.Abstractions
{
	public interface ICommand : IRequest<Result> { }

	public interface ICommand<TResponse> : IRequest<Result<TResponse>> { }
}
