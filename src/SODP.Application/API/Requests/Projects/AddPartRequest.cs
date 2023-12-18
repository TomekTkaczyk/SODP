using MediatR;

namespace SODP.Application.API.Requests.Projects;

public sealed record AddPartRequest : IRequest
{
	public int ProjectId { get; init; }
	public string Sign {  get; init; }
	public string Title { get; init; }

    public AddPartRequest(int projectId, string sign, string title = "")
    {                                                             
        ProjectId = projectId;
        Sign = sign.ToUpper();
        Title = title.ToUpper();
    }
}
