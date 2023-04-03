using SODP.Application.Abstractions;
using SODP.Shared.Response;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Projects;

public sealed class AddPartCommandHandler : ICommandHandler<AddPartCommand>
{
	public Task<ApiResponse> Handle(AddPartCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
