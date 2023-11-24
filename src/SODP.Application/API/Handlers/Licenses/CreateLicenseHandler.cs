using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Licenses;
using SODP.Application.Specifications.Designers;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Licenses;

public sealed class CreateLicenseHandler : IRequestHandler<CreateLicenseRequest, ApiResponse<LicenseDTO>>
{
	private readonly IDesignerRepository _designerRepository;
	private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

    public CreateLicenseHandler(
        IDesignerRepository designerRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper)
    {
		_designerRepository = designerRepository;
		_unitOfWork = unitOfWork;
		_mapper = mapper;
    }

    public async Task<ApiResponse<LicenseDTO>> Handle(CreateLicenseRequest request, CancellationToken cancellationToken)
    {
        var designer = await _designerRepository
            .Get(new DesignerByIdWithLicensesSpecification(request.DesignerId))
            .SingleOrDefaultAsync(x => x.Id == request.DesignerId, cancellationToken)
            ?? throw new NotFoundException(nameof(Designer));
		
        var license = designer.AddLicense(License.Create(designer, request.Content));

        _designerRepository.Update(designer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse.Success(_mapper.Map<LicenseDTO>(license));
    }
}
