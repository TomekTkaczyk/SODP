using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Projects;

public sealed class UpdateProjectCommndHandler : IRequestHandler<UpdateProjectCommand>
{
	public Task<Unit> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
