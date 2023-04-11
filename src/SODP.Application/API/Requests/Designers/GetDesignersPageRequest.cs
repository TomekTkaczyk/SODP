using MediatR;
using SODP.Domain.Entities;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Designers;

public sealed record GetDesignersPageRequest(
    bool? ActiveStatus,
    string SearchString,
    int PageNumber,
    int PageSize) : IRequest<Page<Designer>>
{ }
