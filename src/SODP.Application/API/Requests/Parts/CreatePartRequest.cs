using MediatR;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Parts;

public sealed record CreatePartRequest : IRequest<ApiResponse<int>>
{
	private string _sign;
	private string _title;

	public string Sign { get => _sign; init => _sign = value?.ToUpper(); }
	public string Title { get => _title; init => _title = value?.ToUpper(); }
}
