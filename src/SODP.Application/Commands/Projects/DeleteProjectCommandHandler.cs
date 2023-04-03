using Microsoft.EntityFrameworkCore;
using SODP.Application.Abstractions;
using SODP.Domain.Repositories;
using SODP.Domain.Shared;
using SODP.Infrastructure.Specifications.Projects;
using SODP.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Projects
{
	public sealed class DeleteProjectCommandHandler : ICommandHandler<DeleteProjectCommand>
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IProjectRepository _projectRepository;

		public DeleteProjectCommandHandler(
			IUnitOfWork unitOfWork,
			IProjectRepository projectRepository)
        {
			_unitOfWork = unitOfWork;
			_projectRepository = projectRepository;
		}
        public async Task<ApiResponse> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
		{
			Error error;

			var project = await _projectRepository
				.ApplySpecyfication(new ProjectByIdSpecification(request.Id))
				.SingleOrDefaultAsync(cancellationToken);

			if(project is null)
			{
				error = new Error("DeleteProject", $"Project Id:{request.Id} not found.", HttpStatusCode.NotFound);
				return ApiResponse.Failure(error, HttpStatusCode.NotFound);	
			}

			_projectRepository.Delete(project);
			var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

			if (result == 0)
			{
				error = new Error("Project.Delete", "Projector not found.");
				return ApiResponse.Failure(error, HttpStatusCode.NotFound);
			}

			return ApiResponse.Success(HttpStatusCode.NoContent);
		}
	}
}
