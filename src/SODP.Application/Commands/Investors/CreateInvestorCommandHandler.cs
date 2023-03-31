using SODP.Application.Abstractions;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Investors;

public sealed class CreateInvestorCommandHandler : ICommandHandler<CreateInvestorCommand, Investor>
{
	private readonly IInvestorRepository _investorRepository;
	private readonly IUnitOfWork _unitOfWork;

	public CreateInvestorCommandHandler(IInvestorRepository investorRepository, IUnitOfWork unitOfWork)
	{
		_investorRepository = investorRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<ApiResponse<Investor>> Handle(CreateInvestorCommand request, CancellationToken cancellationToken)
	{
		var investor = await _investorRepository.GetByNameAsync(request.Name, cancellationToken);
		if (investor != null)
		{
			return ApiResponse.Failure<Investor>(new Error("CreateInvestor",$"Investor {request.Name} already exist."));
		}
		investor = _investorRepository.Add(Investor.Create(request.Name));
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return ApiResponse.Success(investor);
	}
}
