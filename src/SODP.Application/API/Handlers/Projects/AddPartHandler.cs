using MediatR;
using SODP.Application.API.Requests.Projects;
using SODP.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

public sealed class AddPartHandler : IRequestHandler<AddPartRequest, Part>
{
    public Task<Part> Handle(AddPartRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
