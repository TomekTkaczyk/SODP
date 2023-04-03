using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Application.Specifications.Investors;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Queries.Investors;

public sealed class GetInvestorByIdQueryHandler : IQueryHandler<GetInvestorByIdQuery, InvestorDTO>
{
	private readonly IInvestorRepository _investorRepository;
	private readonly IMapper _mapper;

	public GetInvestorByIdQueryHandler(
		IInvestorRepository investorRepository,
		IMapper mapper)
    {
		_investorRepository = investorRepository;
		_mapper = mapper;
	}

    public async Task<ApiResponse<InvestorDTO>> Handle(
		GetInvestorByIdQuery request, 
		CancellationToken cancellationToken)
	{

		var investor = await _investorRepository
			.ApplySpecyfication(new InvestorByIdSpecification(request.Id))
			.SingleOrDefaultAsync(cancellationToken);

		if (investor is null)
		{
			var error = new Error("Investor.NotFound", $"The investor with Id:{request.Id} was not found.", HttpStatusCode.NotFound);
			return ApiResponse.Failure<InvestorDTO>(error, HttpStatusCode.NotFound);
		}

		return ApiResponse.Success(_mapper.Map<InvestorDTO>(investor));
	}
}
