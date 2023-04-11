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

internal class SetActiveStatusBranchCommandHandler : IRequestHandler<SetActiveStatusBranchCommand>
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

    public async Task<Unit> Handle(SetActiveStatusBranchCommand request, CancellationToken cancellationToken)
	{
		var investor = await _branchRepository
			.ApplySpecyfication(new BranchByIdSpecification(request.Id))
			.SingleOrDefaultAsync(cancellationToken);

		if (investor is null)
		{
			throw new NotFoundException("Branch");
		}

		investor.SetActiveStatus(request.ActiveStatus);
		_branchRepository.Update(investor);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return new Unit();
	}
}
