using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Designers;
using SODP.Application.Specifications.Designers;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Designers;

public class ChangeDesignerNameHandler : IRequestHandler<ChangeDesignerNameRequest>
{
	private readonly IDesignerRepository _designerRepository;
	private readonly IUnitOfWork _unitOfWork;

	public ChangeDesignerNameHandler(
		IDesignerRepository designerRepository,
		IUnitOfWork unitOfWork)
	{
		_designerRepository = designerRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<Unit> Handle(ChangeDesignerNameRequest request, CancellationToken cancellationToken)
	{
		var existDesigner = await _designerRepository
			.ApplySpecyfication(new DesignerByNameAndDifferentIdSpecification(request.Id,request.Firstname,request.Lastname))
			.AnyAsync(cancellationToken);


		if(existDesigner) 
		{
			throw new ConflictException(nameof(Designer));
		}

		var designer = await _designerRepository
			.GetAll()
			.SingleOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

		designer.Title = request.Title;
		designer.Firstname = request.Firstname;
		designer.Lastname = request.Lastname;

		_designerRepository.Update(designer);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return new Unit();
	}
}
