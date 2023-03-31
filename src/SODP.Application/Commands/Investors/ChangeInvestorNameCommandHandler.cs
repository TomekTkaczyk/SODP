using SODP.Application.Abstractions;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
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

		public async Task<ApiResponse> Handle(ChangeInvestorNameCommand request, CancellationToken cancellationToken)
		{
			var investor = await _investorRepository.GetByNameAsync(request.Name, cancellationToken);
			if((investor is not null) && (investor.Id != request.Id))
			{
				return ApiResponse.Failure(new Error("ChangeInvestorName",$"Investor {request.Name} already exist.")); 
			}

			investor = await _investorRepository.GetByIdAsync(request.Id, cancellationToken);
			if(investor is null)
			{
				return ApiResponse.Failure(new Error("ChangeNameInvestor", $"Investor Id:{request.Id} not found."));
			}

			investor.SetName(request.Name);
			_investorRepository.Update(investor);
			await _unitOfWork.SaveChangesAsync(cancellationToken);

			return ApiResponse.Success();
		}
	}
}
