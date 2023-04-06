using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Application.Specifications.Branches;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Branches;

public class UpdateBranchCommandHandler : ICommandHandler<UpdateBranchCommand>
{
	private readonly IBranchRepository _branchRepository;
	private readonly IUnitOfWork _unitOfWork;

	public UpdateBranchCommandHandler(
        IBranchRepository branchRepository,
        IUnitOfWork unitOfWork)
    {
		_branchRepository = branchRepository;
		_unitOfWork = unitOfWork;
	}

    public async Task<ApiResponse> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
	{
		var branch = await _branchRepository
			.ApplySpecyfication(new BranchByIdSpecification(request.Id))
			.SingleOrDefaultAsync(cancellationToken);

		if(branch is null)
		{
			var error = new Error("","",HttpStatusCode.NotFound);
			return ApiResponse.Failure(error,HttpStatusCode.NotFound);
		}

		_branchRepository.Update(branch);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return ApiResponse.Success(HttpStatusCode.NoContent);
	}
}
