using MediatR;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Stages;

public sealed record UpdateStageBySignRequest : IRequest<ApiResponse>
{
	public string Sign { get; init; }
	public string Title { get; init; }

	public UpdateStageBySignRequest(string sign, string title = "")
	{
		Sign = sign.ToUpper();
		Title = title.ToUpper();
	}
}
