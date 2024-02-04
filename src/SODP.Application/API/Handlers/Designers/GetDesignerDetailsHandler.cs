using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Designers;
using SODP.Application.Specifications.Designers;
using SODP.Domain.Attributes;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Exceptions.DesignerExceptions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Designers;

public class GetDesignerDetailsHandler : IRequestHandler<GetDesignerDetailsRequest, ApiResponse<DesignerDTO>>
{
    private readonly IDesignerRepository _designerRepository;

    public GetDesignerDetailsHandler(IDesignerRepository designerRepository)
    {
        _designerRepository = designerRepository;
    }

    [IgnoreMethodAsyncNameConvention]
    public async Task<ApiResponse<DesignerDTO>> Handle(
        GetDesignerDetailsRequest request,
        CancellationToken cancellationToken)
    {
        var designer = await _designerRepository
            .GetDetailsAsync(request.DesignerId, cancellationToken)
            ?? throw new DesignerNotFoundException();

        var licenses = designer.Licenses.Select(x => new LicenseDTO(
            x.Id,
            null,
            x.Content,
            x.Branches.Select(y => new BranchDTO(
                y.BranchId,
                y.Branch.Sign,
                y.Branch.Title,
                0,
                true,
                new List<LicenseDTO>())
            ))).ToList();

        DesignerDTO designerDTO = new(
            designer.Id,
            designer.Title,
            designer.Firstname,
            designer.Lastname,
            designer.ActiveStatus,
            licenses);

        return ApiResponse.Success(designerDTO);
    }
}
