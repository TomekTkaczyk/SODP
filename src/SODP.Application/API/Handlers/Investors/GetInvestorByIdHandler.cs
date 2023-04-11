using AutoMapper;
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

public sealed class GetInvestorByIdHandler : IRequestHandler<GetInvestorByIdRequest, Investor>
{
    private readonly IInvestorRepository _investorRepository;

    public GetInvestorByIdHandler(
        IInvestorRepository investorRepository)
    {
        _investorRepository = investorRepository;
    }

    public async Task<Investor> Handle(
        GetInvestorByIdRequest request,
        CancellationToken cancellationToken)
    {
        var investor = await _investorRepository
            .ApplySpecyfication(new InvestorByIdSpecification(request.Id))
            .SingleOrDefaultAsync(cancellationToken);

        return investor ?? throw new NotFoundException("Investor");
    }
}
