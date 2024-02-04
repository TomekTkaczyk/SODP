using AutoMapper;
using MediatR;
using SODP.Application.API.Requests.Branches;
using SODP.Application.Extensions;
using SODP.Application.Mappers;
using SODP.Application.Specifications.Branches;
using SODP.Domain.Attributes;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Branches;

public sealed class GetBranchesPageHandler : IRequestHandler<GetBranchesPageRequest, ApiResponse<Page<BranchDTO>>>
{
    private readonly IBranchRepository _branchRepository;

	public GetBranchesPageHandler(IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse<Page<BranchDTO>>> Handle(
        GetBranchesPageRequest request,
        CancellationToken cancellationToken)
    {
        var specification = new BranchSearchSpecification(
                request.ActiveStatus,
                request.SearchString);

        var page = await _branchRepository
            .Get(specification)
            .Select(x => x.ToDTO())
            .AsPageAsync(request.PageNumber, request.PageSize, cancellationToken);

        return ApiResponse.Success(page);
    }
}
