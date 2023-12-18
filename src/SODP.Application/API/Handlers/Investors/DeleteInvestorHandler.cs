using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Investors;
using SODP.Application.Specifications.Common;
using SODP.Application.Specifications.Investors;
using SODP.Domain.Attributes;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Investors;

public sealed class DeleteInvestorHandler : IRequestHandler<DeleteInvestorRequest>
{
    private readonly IInvestorRepository _investorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteInvestorHandler(
        IInvestorRepository investorRepository,
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _investorRepository = investorRepository;
    }

	[IgnoreMethodAsyncNameConvention]
	public async Task<Unit> Handle(DeleteInvestorRequest request, CancellationToken cancellationToken)
    {
        var investor = await _investorRepository
            .Get(new ByIdSpecification<Investor>(request.Id))
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new InvestorNotFoundException();

        _investorRepository.Delete(investor);

        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (result == 0)
        {
            throw new UnknowDeleteException("Error while deleting investor entity.");
        }

        return new Unit();
    }
}
