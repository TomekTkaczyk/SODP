using SODP.Application.Abstractions;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Investors;

public class SetActiveStatusCommandHandler : ICommandHandler<SetActiveStatusCommand>
{
	private readonly IUnitOfWork _unitOfWork;
	private readonly IInvestorRepository _investorRepository;

	public SetActiveStatusCommandHandler(IUnitOfWork unitOfWork, IInvestorRepository investorRepository)
    {
		_unitOfWork = unitOfWork;
		_investorRepository = investorRepository;
	}

    public async Task<ApiResponse> Handle(SetActiveStatusCommand request, CancellationToken cancellationToken)
	{
		var investor = await _investorRepository.GetByIdAsync(request.Id, cancellationToken);
		if(investor is null)
		{
			return ApiResponse.Failure(new Error("SetActive.Investor","Investor not found."));
		}
		investor.SetActiveStatus(request.ActiveStatus);
		_investorRepository.Update(investor);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return ApiResponse.Success();
	}
}
