using MediatR;
using SODP.Application.Abstractions;

namespace SODP.Application.API.Requests.Investors;

public sealed record DeleteInvestorRequest(int Id) : IRequest;
