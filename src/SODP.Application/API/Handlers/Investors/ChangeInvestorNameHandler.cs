using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Investors;
using SODP.Application.Specifications.Investors;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Investors;

public class ChangeInvestorNameHandler : IRequestHandler<ChangeInvestorNameRequest>
{
    private readonly IInvestorRepository _investorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ChangeInvestorNameHandler(
        IInvestorRepository investorRepository,
        IUnitOfWork unitOfWork)
    {
        _investorRepository = investorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(ChangeInvestorNameRequest request, CancellationToken cancellationToken)
    {
		var investor = await _investorRepository
            .ApplySpecyfication(new InvestorByNameAndDifferentIdSpecification(request.Id, request.Name))
            .SingleOrDefaultAsync(cancellationToken);

        if (investor is not null)
        {
            throw new InvestorExistException();
        }

		investor = await _investorRepository
			.ApplySpecyfication(new InvestorByIdSpecification(request.Id))
			.SingleOrDefaultAsync(cancellationToken);

		if (investor is null)
		{
			throw new NotFoundException("Investor");
		}

		investor.SetName(request.Name);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new Unit();
    }
}
