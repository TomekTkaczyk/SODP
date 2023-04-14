using AutoMapper;
using MediatR;
using SODP.Application.API.Requests.Licenses;
using SODP.DataAccess.CQRS.Commands;
using SODP.DataAccess.CQRS.Commands.Common;
using SODP.DataAccess.CQRS.Queries;
using SODP.DataAccess.CQRS.Queries.Designers;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Licenses;

public sealed class CreateLicenseHandler : IRequestHandler<CreateLicenseRequest, ApiResponse<LicenseDTO>>
{
    private readonly IMapper _mapper;
    private readonly ICommandExecutor _commandExecutor;
    private readonly IQueryExecutor _queryExecutor;

    public CreateLicenseHandler(
        IMapper mapper,
        ICommandExecutor commandExecutor,
        IQueryExecutor queryExecutor)
    {
        _mapper = mapper;
        _commandExecutor = commandExecutor;
        _queryExecutor = queryExecutor;
    }

    public async Task<ApiResponse<LicenseDTO>> Handle(CreateLicenseRequest request, CancellationToken cancellationToken)
    {
        var query = new GetDesignerQuery(request.DesignerId);
        var designer = await _queryExecutor.ExecuteAsync(query, cancellationToken);

        if (designer is null)
        {
            throw new NotFoundException(nameof(Designer));
        }

        designer.AddLicense(License.Create(designer, request.Content));

        var command = new UpdateCommand<Designer>(designer);
        await _commandExecutor.ExecuteAsync(command, cancellationToken);

        var license = designer.Licenses.First(x => x.Content.Equals(request.Content));
        if (license is null)
        {
            throw new Exception(nameof(License));
        }

        return ApiResponse.Success(_mapper.Map<LicenseDTO>(license));
    }
}
