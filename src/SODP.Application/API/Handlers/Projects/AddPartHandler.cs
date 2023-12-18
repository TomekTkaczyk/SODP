using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using SODP.Application.API.Requests.Projects;
using SODP.Domain.Attributes;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions.PartExceptions;
using SODP.Domain.Exceptions.ProjectExceptions;
using SODP.Domain.Repositories;
using SODP.Infrastructure.Specifications.Projects;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;

public sealed class AddPartHandler : IRequestHandler<AddPartRequest>
{
    private readonly IProjectRepository _projectRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddPartHandler(
        IProjectRepository projectRepository,
        IUnitOfWork unitOfWork)
    {
        _projectRepository = projectRepository;
        _unitOfWork = unitOfWork;
    }


	[IgnoreMethodAsyncNameConvention]
	public async Task<Unit> Handle(AddPartRequest request, CancellationToken cancellationToken)
    {
        if ( string.IsNullOrWhiteSpace(request.Sign))
        {
            throw new PartSignIsInvalidException();
        }

        var project = await _projectRepository
            .Get(new ProjectWithDetailsSpecification(request.ProjectId))
            .SingleOrDefaultAsync(cancellationToken) 
            ?? throw new ProjectNotFoundException();

        project.AddPart(request.Sign, request.Title);

        _projectRepository.Update(project);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new Unit();
    }
}
