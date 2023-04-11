using MediatR;
using SODP.Domain.Entities;

namespace SODP.Application.API.Requests.Projects;

public sealed record CreateProjectRequest(
    string Number,
    string StageSign,
    string Name,
    string Description) : IRequest<Project>;
