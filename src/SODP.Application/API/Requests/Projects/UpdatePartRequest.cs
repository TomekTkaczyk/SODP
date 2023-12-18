using MediatR;

namespace SODP.Application.API.Requests.Projects;

public sealed record UpdatePartRequest : IRequest
{
	public int Id { get; set; }
	public int ProjectId { get; init; }
	public string Sign { get; init; }
	public string Title { get; init; }

	public UpdatePartRequest(int id, int projectId, string sign, string title = "")
	{
		Id = id;
		ProjectId = projectId;
		Sign = sign.ToUpper();
		Title = title.ToUpper();
	}
}
