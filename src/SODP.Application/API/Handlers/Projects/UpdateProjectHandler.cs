using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Projects;
using SODP.Domain.Exceptions.ProjectExceptions;
using SODP.Domain.Repositories;
using SODP.Infrastructure.Specifications.Projects;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

internal sealed class UpdateProjectHandler : IRequestHandler<UpdateProjectRequest>
{
	private readonly IProjectRepository _projectRepository;
	private readonly IUnitOfWork _unitOfWork;

	public UpdateProjectHandler(
        IProjectRepository projectRepository,
		IUnitOfWork unitOfWork)
    {
		_projectRepository = projectRepository;
		_unitOfWork = unitOfWork;
	}

    public async Task<Unit> Handle(UpdateProjectRequest request, CancellationToken cancellationToken)
    {
        var specyfication = new ProjectByIdSpecification(request.Id);

        var project = await _projectRepository.Get(specyfication)
            .FirstOrDefaultAsync(cancellationToken)
            ?? throw new ProjectNotFoundException();

        project.Update(request.Project);

        _projectRepository.Update(project);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

		return new Unit();
	}
}
