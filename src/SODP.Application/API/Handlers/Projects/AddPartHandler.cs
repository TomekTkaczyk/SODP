using MediatR;
using SODP.Application.API.Requests.Projects;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

public sealed class AddPartHandler : IRequestHandler<AddPartRequest, ApiResponse<PartDTO>>
{
    public Task<ApiResponse<PartDTO>> Handle(AddPartRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
