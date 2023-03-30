using SODP.Application.Abstractions;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Investors;

public sealed class DeleteInvestorCommandHandler : ICommandHandler<DeleteInvestorCommand>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IInvestorRepository _investorRepository;

	public DeleteInvestorCommandHandler(IUnitOfWork unitOfWork, IInvestorRepository investorRepository)
    {
		_unitOfWork = unitOfWork;
		_investorRepository = investorRepository;
	}
    public async Task<ApiResponse> Handle(DeleteInvestorCommand request, CancellationToken cancellationToken)
	{
		var investor = await _investorRepository.GetByIdAsync(request.Id, cancellationToken);
		if (investor is not null)
		{
			_investorRepository.Remove(investor);
			var result = await _unitOfWork.SaveChangesAsync(cancellationToken);
			if(result > 0)
			{
				return ApiResponse.Success();
			}
			return ApiResponse.Failure(new Error("Investor.Delete", "Unknow delete error."));
		}

		return ApiResponse.Failure(new Error("Investor.Delete", "Investor not found."));
	}
}
