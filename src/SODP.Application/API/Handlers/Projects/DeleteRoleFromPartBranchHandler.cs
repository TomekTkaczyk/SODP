using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Projects;
using SODP.Application.Specifications.Projects;
using SODP.Domain.Exceptions.ProjectExceptions;
using SODP.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects;
internal class DeleteRoleFromPartBranchHandler : IRequestHandler<DeleteRoleFromPartBranchRequest> 
{
    private readonly IPartBranchRepository _partBranchRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteRoleFromPartBranchHandler(
        IPartBranchRepository partBranchRepository,
        IUnitOfWork unitOfWork)
    {
        _partBranchRepository = partBranchRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Unit> Handle(DeleteRoleFromPartBranchRequest request, CancellationToken cancellationToken = default) 
    {
        var partBranch = await _partBranchRepository.Get(new PartBranchWithDetailsSpecification(request.PartBranchId))
            .SingleOrDefaultAsync(cancellationToken)
            ?? throw new PartBranchNotFoundException();
        
        partBranch.RemoveRole(request.Role);

        _partBranchRepository.Update(partBranch);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new Unit();
    }
}
