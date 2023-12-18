using MediatR;
using SODP.Application.API.Requests.Licenses;
using SODP.Domain.Attributes;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Licenses;

public sealed class ChangeLicenseContentHandler : IRequestHandler<ChangeLicenseContentRequest>
{
    public ChangeLicenseContentHandler()
    {
        
    }

	[IgnoreMethodAsyncNameConvention]
	public Task<Unit> Handle(ChangeLicenseContentRequest request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
