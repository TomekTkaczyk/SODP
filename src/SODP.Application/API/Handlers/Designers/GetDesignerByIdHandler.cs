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

public sealed class GetDesignerByIdHandler : IRequestHandler<GetDesignerByIdRequest, Designer>
{
    private readonly IDesignerRepository _designerRepository;

    public GetDesignerByIdHandler(
        IDesignerRepository designerRepository)
    {
        _designerRepository = designerRepository;
    }

    public async Task<Designer> Handle(GetDesignerByIdRequest request, CancellationToken cancellationToken)
    {
        var designer = await _designerRepository
            .ApplySpecyfication(new ByIdSpecification<Designer>(request.Id))
            .SingleOrDefaultAsync(cancellationToken);

        if (designer is null)
        {
            throw new NotFoundException(nameof(Designer));
        }

        return designer;
    }
}
