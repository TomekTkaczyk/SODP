using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Designers;

public sealed record GetDesignerWithDetailsRequest(int DesignerId) : IRequest<ApiResponse<DesignerLicensesDTO>>;
