using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Investors;
using SODP.Application.Specifications.Common;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Investors;

public sealed class GetInvestorHandler : IRequestHandler<GetInvestorRequest, ApiResponse<InvestorDTO>>
{
    private readonly IInvestorRepository _investorRepository;
	private readonly IMapper _mapper;

	public GetInvestorHandler(
        IInvestorRepository investorRepository,
        IMapper mapper)
    {
        _investorRepository = investorRepository;
		_mapper = mapper;
	}

    public async Task<ApiResponse<InvestorDTO>> Handle(
        GetInvestorRequest request,
        CancellationToken cancellationToken)
    {
        var investor = await _investorRepository
            .ApplySpecyfication(new ByIdSpecification<Investor>(request.Id))
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new NotFoundException("Investor");

		return ApiResponse.Success(_mapper.Map<InvestorDTO>(investor));
    }
}
