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
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Designers;

public class GetDesignerDetailsHandler : IRequestHandler<GetDesignerDetailsRequest, ApiResponse<DesignerDTO>>
{
    private readonly IDesignerRepository _designerRepository;
    private readonly IMapper _mapper;

    public GetDesignerDetailsHandler(
        IDesignerRepository designerRepository,
        IMapper mapper)
    {
        _designerRepository = designerRepository;
        _mapper = mapper;
    }

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse<DesignerDTO>> Handle(
        GetDesignerDetailsRequest request, 
        CancellationToken cancellationToken)
    {
        var designer = await _designerRepository
            .GetDetailsAsync(request.DesignerId, cancellationToken)
            ?? throw new DesignerNotFoundException();

        return ApiResponse.Success(_mapper.Map<DesignerDTO>(designer));
    }
}
