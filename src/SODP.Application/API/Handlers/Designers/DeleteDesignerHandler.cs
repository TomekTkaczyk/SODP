using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Designers;
using SODP.Application.Specifications.Common;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Designers;

public sealed class DeleteDesignerHandler : IRequestHandler<DeleteDesignerRequest>
{
    private readonly IDesignerRepository _designerRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteDesignerHandler(
        IDesignerRepository designerRepository,
        IUnitOfWork unitOfWork)
    {
        _designerRepository = designerRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(
        DeleteDesignerRequest request,
        CancellationToken cancellationToken)
    {
        var designer = await _designerRepository
            .ApplySpecyfication(new ByIdSpecification<Designer>(request.Id))
            .SingleOrDefaultAsync(cancellationToken);

        if (designer is null)
        {
            throw new NotFoundException(nameof(Designer));
        }

        _designerRepository.Delete(designer);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new Unit();
    }
}
