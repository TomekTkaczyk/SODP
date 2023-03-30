using SODP.Application.Abstractions;
using SODP.Shared.DTO;

namespace SODP.Application.Queries.Investors;

public sealed record GetInvestorByIdQuery(int Id) : IQuery<InvestorDTO>
{
}
