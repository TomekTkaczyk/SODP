using MediatR;
using SODP.Domain.Entities;

namespace SODP.Application.API.Requests.Branches;

public sealed record CreateBranchRequest(
    string Sign,
    string Name) : IRequest<Branch>;
