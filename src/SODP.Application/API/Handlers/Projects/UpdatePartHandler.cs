using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Application.API.Requests.Projects;
using SODP.Domain.Attributes;
using SODP.Domain.Exceptions.ProjectExceptions;
using SODP.Domain.Repositories;
using SODP.Infrastructure.Specifications.Projects;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

internal sealed class UpdatePartHandler : IRequestHandler<UpdatePartRequest>
{
	private readonly IProjectRepository _projectRepository;
	private readonly IFolderManager _folderManager;
	private readonly IUnitOfWork _unitOfWork;

	public UpdatePartHandler(
        IProjectRepository projectRepository,
        IFolderManager folderManager,
		IUnitOfWork unitOfWork)
    {
		_projectRepository = projectRepository;
		_folderManager = folderManager;
		_unitOfWork = unitOfWork;
	}

	[IgnoreMethodAsyncNameConvention]
	public async Task<Unit> Handle(UpdatePartRequest request, CancellationToken cancellationToken)
    {
        var project = await _projectRepository
			.Get(new ProjectWithDetailsSpecification(request.ProjectId))
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new ProjectNotFoundException();

		project.UpdatePart(request.Id, request.Sign, request.Title);

		_projectRepository.Update(project);

		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return new Unit();
	}
}
