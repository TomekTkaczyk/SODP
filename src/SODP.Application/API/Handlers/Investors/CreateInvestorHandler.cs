using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Investors;
using SODP.Application.Specifications.Investors;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Investors;

public sealed class CreateInvestorHandler : IRequestHandler<CreateInvestorRequest, ApiResponse<InvestorDTO>>
{
	private readonly IMapper _mapper;
	private readonly IInvestorRepository _investorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateInvestorHandler(
        IMapper mapper,
        IInvestorRepository investorRepository,
        IUnitOfWork unitOfWork)
    {
		_mapper = mapper;
		_investorRepository = investorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse<InvestorDTO>> Handle(CreateInvestorRequest request, CancellationToken cancellationToken)
    {
        var investorExist = await _investorRepository
			.Get(new InvestorSearchSpecification(null, request.Name))
            .AnyAsync(cancellationToken);

        if (investorExist)
        {
            throw new ConflictException("Investor");
        }

        var investor = _investorRepository.Add(Investor.Create(request.Name));
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse.Success(_mapper.Map<InvestorDTO>(investor));
    }
}
