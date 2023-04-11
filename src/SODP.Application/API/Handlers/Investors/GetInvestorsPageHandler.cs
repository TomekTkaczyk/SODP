using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Investors;
using SODP.Application.Specifications.Investors;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Investors;

public sealed class GetInvestorsPageHandler : IRequestHandler<GetInvestorsPageRequest, Page<Investor>>
{
    private readonly IInvestorRepository _investorRepository;

    public GetInvestorsPageHandler(
        IInvestorRepository investorRepository)
    {
        _investorRepository = investorRepository;
    }

    public async Task<Page<Investor>> Handle(
        GetInvestorsPageRequest request,
        CancellationToken cancellationToken)
    {
        var queryable = _investorRepository
            .ApplySpecyfication(new InvestorSearchSpecification(request.ActiveStatus, request.SearchString));

        var totalItems = await queryable.CountAsync(cancellationToken);

        if (request.PageSize > 0)
        {
            queryable = _investorRepository.GetPageQuery(queryable, request.PageNumber, request.PageSize);
        }

        var collection = new ReadOnlyCollection<Investor>(await queryable.ToListAsync(cancellationToken));

        return Page<Investor>.Create(
                collection,
                request.PageNumber,
                request.PageSize,
                totalItems);
    }
}
