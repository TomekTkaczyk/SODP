using MediatR;
using SODP.Domain.Entities;

namespace SODP.Application.API.Requests.Designers;

public record CreateDesignerRequest(
    string Title,
    string Firstname,
    string Lastname) : IRequest<Designer>;
