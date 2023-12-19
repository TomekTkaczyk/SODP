using MediatR;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Parts;

public sealed record UpdatePartByIdRequest : IRequest<ApiResponse>
{
	private string _title;

	public int Id { get; init; }
	public string Title { get => _title; init => _title = value?.ToUpper(); }
}
