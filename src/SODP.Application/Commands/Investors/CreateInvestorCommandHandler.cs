using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Application.Specifications.Investors;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Investors;

public sealed class CreateInvestorCommandHandler : ICommandHandler<CreateInvestorCommand, Investor>
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

	public async Task<ApiResponse<Investor>> Handle(CreateInvestorCommand request, CancellationToken cancellationToken)
	{
		var investorExist = await _branchRepository
			.ApplySpecyfication(new InvestorByNameSpecification(null, request.Name))
			.AnyAsync(cancellationToken);

		if (investorExist)
		{
			var error = new Error("CreateInvestor", $"Investor {request.Name} already exist.", HttpStatusCode.Conflict);
			return ApiResponse.Failure<Investor>(error, HttpStatusCode.Conflict);
		}

		var investor = _branchRepository.Add(Investor.Create(request.Name));
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return ApiResponse.Success(investor, HttpStatusCode.Created);
	}
}
