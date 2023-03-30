using MediatR;
using SODP.Shared.Response;

namespace SODP.Application.Abstractions;

public interface IQueryHandler<TQuery,TResponse> 
	: IRequestHandler<TQuery,ApiResponse<TResponse>> 
	where TQuery : IQuery<TResponse>
{
}
