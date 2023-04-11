using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Specifications.Designers;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Queries.Designers;

public class GetDesignerWithDetailsQueryHandler : IRequestHandler<GetDesignerWithDetailsQuery, Designer>
{
	private readonly IDesignerRepository _designerRepository;
	private readonly IMapper _mapper;

	public GetDesignerWithDetailsQueryHandler(
        IDesignerRepository designerRepository,
		IMapper mapper)
    {
		_designerRepository = designerRepository;
		_mapper = mapper;
	}
    public async Task<Designer> Handle(GetDesignerWithDetailsQuery request, CancellationToken cancellationToken)
	{
		var designer = await _designerRepository
			.ApplySpecyfication(new DesignerLicensesSpecification(request.DesignerId))
			.SingleOrDefaultAsync(cancellationToken);

		if(designer is null)
		{
			throw new NotFoundException(nameof(Designer));
		}

		return designer;
	}
}
