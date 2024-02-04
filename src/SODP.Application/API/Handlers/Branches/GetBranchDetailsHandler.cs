using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Branches;
using SODP.Application.Specifications.Common;
using SODP.Domain.Attributes;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Branches;

public sealed class GetBranchDetailsHandler : IRequestHandler<GetBranchDetailsRequest, ApiResponse<BranchDTO>>
{
    private readonly IBranchRepository _branchRepository;

    public GetBranchDetailsHandler(IBranchRepository branchRepository)
    {
        _branchRepository = branchRepository;
    }

    [IgnoreMethodAsyncNameConvention]
    public async Task<ApiResponse<BranchDTO>> Handle(
        GetBranchDetailsRequest request,
        CancellationToken cancellationToken)
    {
        var specification = new ByIdSpecification<Branch>(request.Id);
        var branch = await _branchRepository
            .Get(specification)
            .Include(x => x.Licenses)
            .ThenInclude(s => s.License)
            .ThenInclude(s => s.Designer)
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException("Branch");

        var licenses = branch.Licenses.Select(x => new LicenseDTO(
                x.Id,
                new DesignerDTO(
                    x.License.DesignerId,
                    x.License.Designer.Title,
                    x.License.Designer.Firstname,
                    x.License.Designer.Lastname,
                    x.License.Designer.ActiveStatus,
                    new List<LicenseDTO>()),
                x.License.Content,
                new List<BranchDTO>()));

        var branchDTO = new BranchDTO(
            branch.Id,
            branch.Sign,
            branch.Title,
            branch.Order,
            branch.ActiveStatus,
            licenses);

        return ApiResponse.Success(branchDTO);
    }
}