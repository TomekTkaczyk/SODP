using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Specifications.Investors;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Investors;

public sealed class CreateInvestorCommandHandler : IRequestHandler<CreateInvestorCommand, Investor>
{
	private readonly IInvestorRepository _branchRepository;
	private readonly IUnitOfWork _unitOfWork;

	public CreateInvestorCommandHandler(
		IInvestorRepository investorRepository, 
		IUnitOfWork unitOfWork)
	{
		_branchRepository = investorRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<Investor> Handle(CreateInvestorCommand request, CancellationToken cancellationToken)
	{
		var investorExist = await _branchRepository
			.ApplySpecyfication(new InvestorSearchSpecification(null, request.Name))
			.AnyAsync(cancellationToken);

		if (investorExist)
		{
			throw new ConflictException($"Investor with this name");
		}

		var investor = _branchRepository.Add(Investor.Create(request.Name));
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return investor;
	}
}
