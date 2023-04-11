using MediatR;
using SODP.Domain.Entities;

namespace SODP.Application.API.Requests.Stages;

public record GetProjectByIdRequest(
    int Id) : IRequest<Project>;
