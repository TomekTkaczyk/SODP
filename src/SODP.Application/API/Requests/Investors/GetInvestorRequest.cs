using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Investors;

public sealed record GetInvestorRequest(int Id) : IRequest<ApiResponse<InvestorDTO>>;
