using MediatR;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Stages;

public sealed record UpdateStageByIdRequest : IRequest<ApiResponse>
{
	public int Id { get; init; }
	public string Title { get; init; }

    public UpdateStageByIdRequest(int id, string title = "")
    {
        Id = id;
        Title = title.ToUpper();
    }
}
