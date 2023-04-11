using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Specifications.Common;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Queries.Designers;

public sealed class GetDesignerByIdQueryHandler : IRequestHandler<GetDesignerByIdQuery, Designer>
{
	private readonly IDesignerRepository _designerRepository;

	public GetDesignerByIdQueryHandler(
        IDesignerRepository designerRepository)
    {
		_designerRepository = designerRepository;
	}

    public async Task<Designer> Handle(GetDesignerByIdQuery request, CancellationToken cancellationToken)
	{
		var designer = await _designerRepository
			.ApplySpecyfication(new ByIdSpecification<Designer>(request.Id))
			.SingleOrDefaultAsync(cancellationToken);

		if(designer is null)
		{
			throw new NotFoundException(nameof(Designer));
		}

		return designer;
	}
}
