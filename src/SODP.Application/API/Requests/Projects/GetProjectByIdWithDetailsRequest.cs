using MediatR;
using SODP.Domain.Entities;

namespace SODP.Application.API.Requests.Projects;

public record GetProjectByIdWithDetailsRequest(
    int Id) : IRequest<Project>;
