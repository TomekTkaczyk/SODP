using MediatR;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Parts;

public sealed record UpdatePartBySignRequest : IRequest<ApiResponse>
{
	public string Sign { get; init; }
	public string Title { get; init; }

	public UpdatePartBySignRequest(string sign, string title = "")
	{
		Sign = sign.ToUpper();
		Title = title.ToUpper();
	}
}
