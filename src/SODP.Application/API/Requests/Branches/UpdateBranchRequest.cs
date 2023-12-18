using MediatR;

namespace SODP.Application.API.Requests.Branches;

public sealed record UpdateBranchRequest : IRequest
{
	public int Id { get; init; }
	public string Sign { get; init; }
	public string Title { get; init; }

    public UpdateBranchRequest(int id, string sign, string title = "")
    {   
        Id = id;
        Sign = sign.ToUpper();
        Title = title.ToUpper();
    }
}
