using MediatR;
using SODP.Domain.Shared;

namespace SODP.Application.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}
