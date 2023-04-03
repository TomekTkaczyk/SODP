using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Application.Specifications.Investors;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Investors;

public class SetActiveStatusCommandHandler : ICommandHandler<SetActiveStatusCommand>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IInvestorRepository _investorRepository;

	public SetActiveStatusCommandHandler(
		IUnitOfWork unitOfWork, 
		IInvestorRepository investorRepository)
    {
		_unitOfWork = unitOfWork;
		_investorRepository = investorRepository;
	}

    public async Task<ApiResponse> Handle(SetActiveStatusCommand request, CancellationToken cancellationToken)
	{
		var investor = await _investorRepository
			.ApplySpecyfication(new InvestorByIdSpecification(request.Id))
			.SingleOrDefaultAsync(cancellationToken);
		if(investor is null)
		{
			var error = new Error("SetActive.Investor", "Investor not found.", HttpStatusCode.NotFound);
			return ApiResponse.Failure(error, HttpStatusCode.NotFound);
		}
		investor.SetActiveStatus(request.ActiveStatus);
		_investorRepository.Update(investor);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return ApiResponse.Success(HttpStatusCode.NoContent);
	}
}
