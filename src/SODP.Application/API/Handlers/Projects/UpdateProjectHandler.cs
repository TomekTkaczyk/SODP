using MediatR;
using SODP.Application.API.Requests.Projects;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

public sealed class UpdateProjectHandler : IRequestHandler<UpdateProjectRequest>
{
    public Task<Unit> Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
