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

public sealed class DeleteBranchCommandHandler : ICommandHandler<DeleteBranchCommand>
{
	private readonly IBranchRepository _branchRepository;
	private readonly IUnitOfWork _unitOfWork;

	public DeleteBranchCommandHandler(
        IBranchRepository branchRepository,
        IUnitOfWork unitOfWork)
    {
		_branchRepository = branchRepository;
		_unitOfWork = unitOfWork;
	}

    public async Task<ApiResponse> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
	{
		var branch = await _branchRepository
			.ApplySpecyfication(new BranchByIdSpecification(request.Id))
			.SingleOrDefaultAsync(cancellationToken);

		if(branch is  null)
		{
			var error = new Error("Delete.Branch",$"Branch Id:{request.Id} not found.",HttpStatusCode.NotFound);
			return ApiResponse.Failure(error,HttpStatusCode.NotFound);
		}

		_branchRepository.Delete(branch);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return ApiResponse.Success();
	}
}
