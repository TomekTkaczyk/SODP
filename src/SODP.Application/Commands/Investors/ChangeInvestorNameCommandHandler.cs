using SODP.Application.Abstractions;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Domain.Shared;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Investors
{
	public class ChangeInvestorNameCommandHandler : ICommandHandler<ChangeInvestorNameCommand>
	{
		private readonly IInvestorRepository _investorRepository;
		private readonly IUnitOfWork _unitOfWork;

		public ChangeInvestorNameCommandHandler(IInvestorRepository investorRepository, IUnitOfWork unitOfWork)
		{
			_investorRepository = investorRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result> Handle(ChangeInvestorNameCommand request, CancellationToken cancellationToken)
		{
			var investor = await _investorRepository.GetByNameAsync(request.Name, cancellationToken);
			if(investor != null && investor.Id != request.Id)
			{
				throw new InvestorExistException(); 
			}

			investor = await _investorRepository.GetByIdAsync(request.Id, cancellationToken);
			if(investor == null)
			{
				throw new InvestorNotFoundException();
			}
			investor.SetName(request.Name);

			_investorRepository.Update(investor);
			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return Result.Success();
		}
	}
}
