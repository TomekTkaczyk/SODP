using MediatR;
using SODP.Domain.Shared;

namespace SODP.Application.Abstractions;

public interface IQueryHandler<TQuery,TResponse> 
	: IRequestHandler<TQuery,Result<TResponse>> 
	where TQuery : IQuery<TResponse>
{
}
