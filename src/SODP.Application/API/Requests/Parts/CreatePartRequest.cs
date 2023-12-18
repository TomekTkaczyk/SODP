using MediatR;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Parts;

public sealed record CreatePartRequest : IRequest<ApiResponse<int>>
{
	public string Sign { get; init; }
	public string Title { get; init; }

    public CreatePartRequest(string sign, string title = "")
    {                                                      
        Sign = sign.ToUpper();
        Title = title.ToUpper();
    }
}
