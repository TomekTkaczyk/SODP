using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Specifications.Branches;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Branches;

public class UpdateBranchCommandHandler : IRequestHandler<UpdateBranchCommand>
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

    public async Task<Unit> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
	{
		var branch = await _branchRepository
			.ApplySpecyfication(new BranchByIdSpecification(request.Id))
			.SingleOrDefaultAsync(cancellationToken);

		if(branch is null)
		{
			throw new NotFoundException("Branch");
		}

		_branchRepository.Update(branch);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return new Unit();
	}
}
