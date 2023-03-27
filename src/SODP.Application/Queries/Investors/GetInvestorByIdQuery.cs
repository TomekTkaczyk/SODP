using SODP.Application.Abstractions;
using SODP.Application.ValueObjects;

namespace SODP.Application.Queries.Investors;

public sealed record GetInvestorByIdQuery(int Id) : IQuery<InvestorValueObject>
{
}
