using AutoMapper;
using MediatR;
using SODP.Application.API.Requests.Designers;
using SODP.DataAccess.CQRS.Commands;
using SODP.DataAccess.CQRS.Commands.Designers;
using SODP.DataAccess.CQRS.Queries;
using SODP.DataAccess.CQRS.Queries.Designers;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Application.API.Handlers.Designers;

public class CreateDesignerHandler : IRequestHandler<CreateDesignerRequest, ApiResponse<DesignerDTO>>
{
	private readonly IMapper _mapper;
	private readonly ICommandExecutor _commandExecutor;
	private readonly IQueryExecutor _queryExecutor;

    public CreateDesignerHandler(
        IMapper mapper,
        ICommandExecutor commandExecutor,
        IQueryExecutor queryExecutor)
    {
		_mapper = mapper;
		_commandExecutor = commandExecutor;
		_queryExecutor = queryExecutor;
    }

    public async Task<ApiResponse<DesignerDTO>> Handle(CreateDesignerRequest request, CancellationToken cancellationToken)
    {
        var query = new GetDesigerByNameQuery(request.Firstname, request.Lastname);
        var designer = await _queryExecutor.ExecuteAsync(query, cancellationToken);
        
        if(designer is not null)
        {
            throw new DesignerConflictException();
        }

        designer = Designer.Create(
            request.Title,
            request.Firstname,
            request.Lastname);

        var command = new CreateDesignerCommand(designer);
        designer = await _commandExecutor.ExecuteAsync(command, cancellationToken);

        return ApiResponse.Success(_mapper.Map<DesignerDTO>(designer));
    }
}
