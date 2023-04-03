using SODP.Application.Abstractions;
using SODP.Shared.Response;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Projects;

public sealed class UpdateProjectCommndHandler : ICommandHandler<UpdateProjectCommand>
{
	public Task<ApiResponse> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
