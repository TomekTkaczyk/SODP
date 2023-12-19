using MediatR;

namespace SODP.Application.API.Requests.Projects;

public sealed record AddPartRequest : IRequest
{
	private string _sign;
	private string _title;

	public int ProjectId { get; init; }
	public string Sign { get => _sign; init => _sign = value?.ToUpper(); }
	public string Title { get => _title; init => _title = value?.ToUpper(); }
}
