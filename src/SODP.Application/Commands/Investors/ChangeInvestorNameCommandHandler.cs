﻿using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Application.Specifications.Investors;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Investors;

public class ChangeInvestorNameCommandHandler : ICommandHandler<ChangeInvestorNameCommand>
{
	private readonly IInvestorRepository _investorRepository;
	private readonly IUnitOfWork _unitOfWork;

	public ChangeInvestorNameCommandHandler(IInvestorRepository investorRepository, IUnitOfWork unitOfWork)
	{
		_investorRepository = investorRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<ApiResponse> Handle(ChangeInvestorNameCommand request, CancellationToken cancellationToken)
	{
		var investor = await _investorRepository
			.ApplySpecyfication(new InvestorByNameAndDifferentIdSpecification(request.Id, request.Name))
			.SingleOrDefaultAsync(cancellationToken);
		
		if (investor is not null)
		{
			var error = new Error(
				"ChangeInvestorName", 
				$"Investor {request.Name} already exist.", 
				HttpStatusCode.Conflict);
			return ApiResponse.Failure(error, HttpStatusCode.Conflict);
		}

		investor.SetName(request.Name);
		_investorRepository.Update(investor);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return ApiResponse.Success();
	}
}