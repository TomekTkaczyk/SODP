using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Projects;
using SODP.Application.Specifications.Common;
using SODP.Application.Specifications.Projects;
using SODP.Domain.Attributes;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions.ProjectExceptions;
using SODP.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

public sealed class AddBranchToPartHandler : IRequestHandler<AddBranchToPartRequest>
{
	private readonly IProjectPartRepository _projectPartRepository;
	private readonly IBranchRepository _branchRepository;
	private readonly IUnitOfWork _unitOfWork;

	public AddBranchToPartHandler(
		IProjectPartRepository projectPartRepository,
		IBranchRepository branchRepository,
		IUnitOfWork unitOfWork)
        {
		_projectPartRepository = projectPartRepository;
		_branchRepository = branchRepository;
		_unitOfWork = unitOfWork;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<Unit> Handle(AddBranchToPartRequest request, CancellationToken cancellationToken)
	{
		var projectPart = await _projectPartRepository
			.Get(new ProjectPartByIdSpecification(request.PartId))
			.SingleOrDefaultAsync(cancellationToken)
			?? throw new ProjectPartNotFoundException();

		var branch = await _branchRepository
			.Get(new ByIdSpecification<Branch>(request.BranchId))
			.SingleOrDefaultAsync(cancellationToken)
			?? throw new ProjectPartNotFoundException();

		var partBranch = PartBranch.Create(projectPart, branch);

		projectPart.AddBranch(partBranch);

		_projectPartRepository.Update(projectPart);

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return new Unit();
	}
}
