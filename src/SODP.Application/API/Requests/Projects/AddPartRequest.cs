using MediatR;
using SODP.Domain.Entities;
using SODP.Shared.DTO;

namespace SODP.Application.API.Requests.Projects;

public sealed record AddPartRequest(
    int Id,
    PartDTO part) : IRequest<Part>;
