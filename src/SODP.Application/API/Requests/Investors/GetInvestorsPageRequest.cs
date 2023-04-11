using MediatR;
using SODP.Domain.Entities;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Investors;

public sealed record GetInvestorsPageRequest(
    bool? ActiveStatus,
    string SearchString,
    int PageNumber,
    int PageSize) : IRequest<Page<Investor>>;
