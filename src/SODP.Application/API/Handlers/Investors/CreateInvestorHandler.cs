using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Investors;
using SODP.Application.Specifications.Investors;
using SODP.Domain.Attributes;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Exceptions.InvestorExceptions;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Investors;

public sealed class CreateInvestorHandler : IRequestHandler<CreateInvestorRequest, ApiResponse<int>>
{
	private readonly IInvestorRepository _investorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateInvestorHandler(
        IInvestorRepository investorRepository,
        IUnitOfWork unitOfWork)
    {
		_investorRepository = investorRepository;
        _unitOfWork = unitOfWork;
    }

	[IgnoreMethodAsyncNameConvention]
	public async Task<ApiResponse<int>> Handle(CreateInvestorRequest request, CancellationToken cancellationToken)
    {
        var investorExist = await _investorRepository
			.Get(new InvestorSearchSpecification(null, request.Name))
            .AnyAsync(cancellationToken);

        if (investorExist)
        {
            throw new InvestorConflictException();
        }

        var investor = _investorRepository.Add(Investor.Create(request.Name));
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return ApiResponse.Success(investor.Id);
    }
}
