using MediatR;
using SODP.Application.Abstractions;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using SODP.Shared.Response;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.Commands.Investors
{
	public class CreateInvestorCommandHandler : ICommandHandler<CreateInvestorCommand, Investor>
	{
		private readonly IInvestorRepository _investorRepository;
		private readonly IUnitOfWork _unitOfWork;

		public CreateInvestorCommandHandler(IInvestorRepository investorRepository, IUnitOfWork unitOfWork)
		{
			_investorRepository = investorRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task<Result<Investor>> Handle(CreateInvestorCommand request, CancellationToken cancellationToken)
		{
			var investor = await _investorRepository.GetByNameAsync(request.Name, cancellationToken);
			if (investor == null)
			{
				investor = _investorRepository.Add(Investor.Create(request.Name));
				await _unitOfWork.SaveChangesAsync(cancellationToken);

				return new Result<Investor>(investor);
			}
			else
			{
				throw new InvestorExistException();
			}
		}
	}
}
