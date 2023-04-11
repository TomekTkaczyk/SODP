using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Designers;

public sealed record CreateDesignerRequest(
    string Title,
    string Firstname,
    string Lastname) : IRequest<ApiResponse<DesignerDTO>>;
