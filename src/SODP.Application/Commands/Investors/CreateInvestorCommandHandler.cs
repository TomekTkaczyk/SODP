//using SODP.Application.Abstractions;
//using SODP.Domain.Entities;
//using SODP.Domain.Repositories;
//using SODP.Shared.Response;
//using System.Threading;
//using System.Threading.Tasks;

//namespace SODP.Application.Commands.Investors
//{
//	internal sealed class CreateInvestorCommandHandler : ICommandHandler<CreateInvestorCommand>
//	{
//		private readonly IInvestorRepository _investorRepository;
//		private readonly IUnitOfWork _unitOfWork;

//		public CreateInvestorCommandHandler(IInvestorRepository investorRepository, IUnitOfWork unitOfWork)
//		{
//			_investorRepository = investorRepository;
//			_unitOfWork = unitOfWork;
//		}

//		public async Task<Result> Handle(CreateInvestorCommand request, CancellationToken cancellationToken)
//		{
//			var investor = new Investor()
//			{
//				Name = request.Name,
//			};

//			_investorRepository.Create(investor);

//			await _unitOfWork.SaveChangesAsync(cancellationToken);

//			return Result.Success();
//		}
//	}
//}
