using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Investors;

public sealed record CreateInvestorRequest(
    string Name) : IRequest<ApiResponse<InvestorDTO>>;
