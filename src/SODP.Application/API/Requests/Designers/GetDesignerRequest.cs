using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Designers;

public record GetDesignerRequest(
	int Id) : IRequest<ApiResponse<DesignerDTO>>;
