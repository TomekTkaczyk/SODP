using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Designers;

public sealed record GetDesignerRequest(int Id) : IRequest<ApiResponse<DesignerDTO>>;
