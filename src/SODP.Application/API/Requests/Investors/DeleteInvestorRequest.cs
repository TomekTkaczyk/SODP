using MediatR;

namespace SODP.Application.API.Requests.Investors;

public sealed record DeleteInvestorRequest(
	int Id) : IRequest;
