using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Application.Specifications.Investors;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Investors;

public sealed class DeleteInvestorCommandHandler : ICommandHandler<DeleteInvestorCommand>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IInvestorRepository _investorRepository;

	public DeleteInvestorCommandHandler(
		IInvestorRepository investorRepository, 
		IUnitOfWork unitOfWork)
    {
		_unitOfWork = unitOfWork;
		_investorRepository = investorRepository;
	}

    public async Task<ApiResponse> Handle(DeleteInvestorCommand request, CancellationToken cancellationToken)
	{
		Error error;
		
		var investor = await _investorRepository
			.ApplySpecyfication(new InvestorByIdSpecification(request.Id))
			.SingleOrDefaultAsync(cancellationToken);

		if (investor is null)
		{
			error = new Error("Investor.Delete", "Unknow delete error.", HttpStatusCode.InternalServerError);
			return ApiResponse.Failure(error, HttpStatusCode.InternalServerError);
		}

		_investorRepository.Delete(investor);
		var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

		if(result == 0)
		{
			error = new Error("Investor.Delete", "Investor not found.");
			return ApiResponse.Failure(error, HttpStatusCode.NotFound);
		}

		return ApiResponse.Success(HttpStatusCode.NoContent);
	}
}
