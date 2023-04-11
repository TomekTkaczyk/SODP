using MediatR;
using SODP.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Projects;

public sealed class AddPartCommandHandler : IRequestHandler<AddPartCommand, Part>
{
	public Task<Part> Handle(AddPartCommand request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
