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

public sealed class CreateInvestorHandler : IRequestHandler<CreateInvestorRequest, Investor>
{
    private readonly IInvestorRepository _branchRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateInvestorHandler(
        IInvestorRepository investorRepository,
        IUnitOfWork unitOfWork)
    {
        _branchRepository = investorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Investor> Handle(CreateInvestorRequest request, CancellationToken cancellationToken)
    {
        var investorExist = await _branchRepository
            .ApplySpecyfication(new InvestorSearchSpecification(null, request.Name))
            .AnyAsync(cancellationToken);

        if (investorExist)
        {
            throw new ConflictException($"Investor with this name");
        }

        var investor = _branchRepository.Add(Investor.Create(request.Name));
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return investor;
    }
}
