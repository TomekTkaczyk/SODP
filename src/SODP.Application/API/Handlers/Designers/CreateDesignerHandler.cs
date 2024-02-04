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
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Designers;

public class CreateDesignerHandler : IRequestHandler<CreateDesignerRequest, ApiResponse<DesignerDTO>>
{
	private readonly IDesignerRepository _designerRepository;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

    public CreateDesignerHandler(
        IDesignerRepository designerRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
		_designerRepository = designerRepository;
		_unitOfWork = unitOfWork;
		_mapper = mapper;
    }

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse<DesignerDTO>> Handle(CreateDesignerRequest request, CancellationToken cancellationToken)
    {
        var designerExist = await _designerRepository
            .Get(new DesignerByNameSpecification(null, request.Firstname, request.Lastname))
            .AnyAsync(cancellationToken);

        if(designerExist)
        {
            throw new DesignerConflictException();
        }

        var designer = Designer.Create(
            request.Title,
            request.Firstname,
            request.Lastname);

        _designerRepository.Add(designer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse.Success(_mapper.Map<DesignerDTO>(designer));
    }
}
