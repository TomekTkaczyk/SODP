using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Application.Specifications.Branches;
using SODP.Application.Specifications.Investors;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Branches;

internal class SetActiveStatusBranchCommandHandler : ICommandHandler<SetActiveStatusBranchCommand>
{
	private readonly IBranchRepository _branchRepository;
	private readonly IUnitOfWork _unitOfWork;

	public SetActiveStatusBranchCommandHandler(
        IBranchRepository branchRepository,
		IUnitOfWork unitOfWork)
    {
		_branchRepository = branchRepository;
		_unitOfWork = unitOfWork;
	}

    public async Task<ApiResponse> Handle(SetActiveStatusBranchCommand request, CancellationToken cancellationToken)
	{
		var investor = await _branchRepository
			.ApplySpecyfication(new BranchByIdSpecification(request.Id))
			.SingleOrDefaultAsync(cancellationToken);
		if (investor is null)
		{
			var error = new Error("SetActive.Branch", "Branch not found.", HttpStatusCode.NotFound);
			return ApiResponse.Failure(error, HttpStatusCode.NotFound);
		}
		investor.SetActiveStatus(request.ActiveStatus);
		_branchRepository.Update(investor);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return ApiResponse.Success(HttpStatusCode.NoContent);
	}
}
