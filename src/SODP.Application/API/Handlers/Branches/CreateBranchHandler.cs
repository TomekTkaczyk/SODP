using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Branches;
using SODP.Application.Specifications.Branches;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Branches;

public sealed class CreateBranchHandler : IRequestHandler<CreateBranchRequest, ApiResponse<BranchDTO>>
{
    private readonly IBranchRepository _branchRepository;
    private readonly IUnitOfWork _unitOfWork;
	private readonly IMapper _mapper;

	public CreateBranchHandler(
        IBranchRepository investorRepository,
        IUnitOfWork unitOfWork,
		IMapper mapper)
    {
        _branchRepository = investorRepository;
        _unitOfWork = unitOfWork;
		_mapper = mapper;
	}

    public async Task<ApiResponse<BranchDTO>> Handle(CreateBranchRequest request, CancellationToken cancellationToken)
    {
        var branchExist = await _branchRepository
            .Get(new BranchByNameSpecification(null, request.Title))
            .AnyAsync(cancellationToken);

        if (branchExist)
        {
            throw new ConflictException("Branch");
        }

        var branch = _branchRepository.Add(Branch.Create(request.Sign, request.Title));
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse.Success(_mapper.Map<BranchDTO>(branch));
    }
}
