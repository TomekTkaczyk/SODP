using AutoMapper;
using MediatR;
using SODP.Application.API.Requests.Licenses;
using SODP.DataAccess.CQRS.Queries;
using SODP.DataAccess.CQRS.Queries.Common;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Licenses;

public sealed class GetLicenseHandler : IRequestHandler<GetLicenseRequest, ApiResponse<LicenseDTO>>
{
	private readonly IQueryExecutor _queryExecutor;
	private readonly IMapper _mapper;

	public GetLicenseHandler(
        IQueryExecutor queryExecutor,
        IMapper mapper)
    {
		_queryExecutor = queryExecutor;
		_mapper = mapper;
	}

    public async Task<ApiResponse<LicenseDTO>> Handle(GetLicenseRequest request, CancellationToken cancellationToken)
	{
		var query = new GetByIdQuery<License>(request.Id);

		var license = await _queryExecutor.ExecuteAsync(query, cancellationToken);
		if(license is null)
		{
			throw new NotFoundException("License");
		}

		return ApiResponse.Success(_mapper.Map<LicenseDTO>(license));
	}
}
