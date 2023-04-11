using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Designers;
using SODP.Application.Specifications.Designers;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Designers;

public class GetDesignerWithDetailsHandler : IRequestHandler<GetDesignerWithDetailsRequest, ApiResponse<DesignerLicensesDTO>>
{
    private readonly IDesignerRepository _designerRepository;
    private readonly IMapper _mapper;

    public GetDesignerWithDetailsHandler(
        IDesignerRepository designerRepository,
        IMapper mapper)
    {
        _designerRepository = designerRepository;
        _mapper = mapper;
    }
    public async Task<ApiResponse<DesignerLicensesDTO>> Handle(GetDesignerWithDetailsRequest request, CancellationToken cancellationToken)
    {
        var designer = await _designerRepository
            .ApplySpecyfication(new DesignerLicensesSpecification(request.DesignerId))
            .SingleOrDefaultAsync(cancellationToken);

        if (designer is null)
        {
            throw new NotFoundException(nameof(Designer));
        }

		var designerLicenses = new DesignerLicensesDTO(
	        _mapper.Map<DesignerDTO>(designer),
	        _mapper.Map<ICollection<LicenseDTO>>(designer.Licenses));


		return ApiResponse.Success(designerLicenses);
    }
}
