using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Specifications.Designers;
using SODP.Application.Specifications.Investors;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Queries.Designers;

public sealed class GetDesignersPageQueryHandler : IRequestHandler<GetDesignersPageQuery, Page<Designer>>
{
	private readonly IDesignerRepository _designerRepository;

	public GetDesignersPageQueryHandler(
		IDesignerRepository designerRepository)
	{
		_designerRepository = designerRepository;
	}

	public async Task<Page<Designer>> Handle(
		GetDesignersPageQuery request,
		CancellationToken cancellationToken)
	{
		var queryable = _designerRepository
			.ApplySpecyfication(new DesignerSearchSpecification(request.ActiveStatus, request.SearchString));

		var totalItems = await queryable.CountAsync(cancellationToken);

		if (request.PageSize > 0)
		{
			queryable = _designerRepository.GetPageQuery(queryable, request.PageNumber, request.PageSize);
		}

		var collection = new ReadOnlyCollection<Designer>(await queryable.ToListAsync(cancellationToken));

		return Page<Designer>.Create(
				collection,
				request.PageNumber,
				request.PageSize,
				totalItems);
	}
}
