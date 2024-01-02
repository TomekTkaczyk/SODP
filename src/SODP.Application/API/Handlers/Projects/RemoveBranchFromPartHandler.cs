using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Projects;
using SODP.Application.Specifications.Projects;
using SODP.Domain.Attributes;
using SODP.Domain.Exceptions.ProjectExceptions;
using SODP.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Projects
{
    internal class RemoveBranchFromPartHandler : IRequestHandler<RemoveBranchFromPartRequest>
    {
        private readonly IPartBranchRepository _partBranchRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveBranchFromPartHandler(
            IPartBranchRepository partBranchRepository,
            IUnitOfWork unitOfWork)
        {
            _partBranchRepository = partBranchRepository;
            _unitOfWork = unitOfWork;
        }

        [IgnoreMethodAsyncNameConvention]
        public async Task<Unit> Handle(RemoveBranchFromPartRequest request, CancellationToken cancellationToken)
        {
            var partBranch = await _partBranchRepository
                .Get(new PartBranchByIdSpecification(request.PartBranchId))
                .SingleOrDefaultAsync(cancellationToken)
                ?? throw new PartBranchNotFoundException();

            _partBranchRepository.Delete(partBranch);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new Unit();
        }
    }
}
