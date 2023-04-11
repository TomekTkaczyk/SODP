using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.Specifications.Designers;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Designers;

public class CreateDesignerCommandHandler : IRequestHandler<CreateDesignerCommand, Designer>
{
	private readonly IDesignerRepository _designerRepository;
	private readonly IUnitOfWork _unitOfWork;

	public CreateDesignerCommandHandler(
		IDesignerRepository designerRepository,
		IUnitOfWork unitOfWork)
	{
		_designerRepository = designerRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<Designer> Handle(CreateDesignerCommand request, CancellationToken cancellationToken)
	{
		var designerExist = await _designerRepository
			.ApplySpecyfication(new DesignerByNameSpecification(null, request.Firstname, request.Lastname))
			.AnyAsync(cancellationToken);

		if (designerExist)
		{
			throw new DesignerConflictException();
		}

		var designer = _designerRepository.Add(Designer.Create(
			request.Title,
			request.Firstname,
			request.Lastname));
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return designer;
	}
}
