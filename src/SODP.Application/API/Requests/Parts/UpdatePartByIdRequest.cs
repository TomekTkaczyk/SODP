using MediatR;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Parts;

public sealed record UpdatePartByIdRequest : IRequest<ApiResponse>
{
	public int Id { get; init; }
	public string Title { get; init;}

    public UpdatePartByIdRequest(int id, string title = "")
    {                                                
        Id = id;
        Title = title.ToUpper();
    }
}
