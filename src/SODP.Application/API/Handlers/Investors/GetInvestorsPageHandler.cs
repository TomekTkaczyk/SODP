using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Investors;
using SODP.Application.Specifications.Investors;
using SODP.Domain.Attributes;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Investors;

public sealed class GetInvestorsPageHandler : IRequestHandler<GetInvestorsPageRequest, ApiResponse<Page<InvestorDTO>>>
{
    private readonly IInvestorRepository _investorRepository;
	private readonly IMapper _mapper;

	public GetInvestorsPageHandler(
        IInvestorRepository investorRepository,
        IMapper mapper)
    {
        _investorRepository = investorRepository;
		_mapper = mapper;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse<Page<InvestorDTO>>> Handle(
        GetInvestorsPageRequest request,
        CancellationToken cancellationToken)
    {
        var specification = new InvestorSearchSpecification(
                request.ActiveStatus,
                request.SearchString);

		var page = await _investorRepository.GetPageAsync(
			specification,
			request.PageNumber,
			request.PageSize,
			cancellationToken);

		return ApiResponse.Success(_mapper.Map<Page<InvestorDTO>>(page));

	}
}
