﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using SODP.Application.API.Requests.Investors;
using SODP.Application.Specifications.Investors;
using SODP.Domain.Exceptions;
using SODP.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Investors;

public sealed class DeleteInvestorHandler : IRequestHandler<DeleteInvestorRequest>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IInvestorRepository _investorRepository;

    public DeleteInvestorHandler(
        IInvestorRepository investorRepository,
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _investorRepository = investorRepository;
    }

    public async Task<Unit> Handle(DeleteInvestorRequest request, CancellationToken cancellationToken)
    {
        var investor = await _investorRepository
            .ApplySpecyfication(new InvestorByIdSpecification(request.Id))
            .SingleOrDefaultAsync(cancellationToken);

        if (investor is null)
        {
            throw new InvestorNotFoundException();
        }

        _investorRepository.Delete(investor);
        var result = await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (result == 0)
        {
            throw new UnknowDeleteException("Error while deleting investor entity.");
        }

        return new Unit();
    }
}