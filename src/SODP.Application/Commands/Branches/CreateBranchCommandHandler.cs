using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Commands.Branches;
using SODP.Application.Specifications.Branches;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Investors;

public sealed class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, Branch>
{
	private readonly IBranchRepository _branchRepository;
	private readonly IUnitOfWork _unitOfWork;

	public CreateBranchCommandHandler(
		IBranchRepository investorRepository, 
		IUnitOfWork unitOfWork)
	{
		_branchRepository = investorRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<Branch> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
	{
		var branchExist = await _branchRepository
			.ApplySpecyfication(new BranchByNameSpecification(null, request.Name))
			.AnyAsync(cancellationToken);

		if (branchExist)
		{
			throw new ConflictException("Branch");
		}

		var branch = _branchRepository.Add(Branch.Create(request.Sign, request.Name));
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return branch;
	}
}
