using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Licenses;
using SODP.DataAccess.CQRS.Queries;
using SODP.DataAccess.CQRS.Queries.Licenses;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Licenses;

public sealed class DeleteLicenseHandler : IRequestHandler<DeleteLicenseRequest>
{
	private readonly IQueryExecutor _queryExecutor;

	public DeleteLicenseHandler(IQueryExecutor queryExecutor)
    {
		_queryExecutor = queryExecutor;
	}

    public async Task<Unit> Handle(DeleteLicenseRequest request, CancellationToken cancellationToken)
	{
		var query = new GetLicenseQuery(request.Id);
		var license = await _queryExecutor.ExecuteAsync(query, cancellationToken);

		return new Unit();
	}
}
