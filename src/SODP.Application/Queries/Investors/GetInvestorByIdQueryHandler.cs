using SODP.Application.Abstractions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Queries.Investors;

public sealed class GetInvestorByIdQueryHandler : IQueryHandler<GetInvestorByIdQuery, InvestorDTO>
{
	private readonly IInvestorRepository _investorRepository;

	public GetInvestorByIdQueryHandler(IInvestorRepository investorRepository)
    {
		_investorRepository = investorRepository;
	}

    public async Task<ApiResponse<InvestorDTO>> Handle(
		GetInvestorByIdQuery request, 
		CancellationToken cancellationToken)
	{
		var investor = await _investorRepository.GetByIdAsync(request.Id, cancellationToken);

		if (investor is null)
		{
			return ApiResponse.Failure<InvestorDTO>(new Error(
				"Investor.NotFound",
				$"The investor with Id:{request.Id} was not found."));
		}

		var response = new InvestorDTO { Id = investor.Id, Name = investor.Name, ActiveStatus = investor.ActiveStatus };

		return ApiResponse.Success(response);
	}
}
