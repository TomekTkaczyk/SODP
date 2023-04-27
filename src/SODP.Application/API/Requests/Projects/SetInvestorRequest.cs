using MediatR;

namespace SODP.Application.API.Requests.Projects;

public sealed record SetInvestorRequest(int Id, string Investor) : IRequest;
