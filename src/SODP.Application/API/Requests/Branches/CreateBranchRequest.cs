using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;

namespace SODP.Application.API.Requests.Branches;

public sealed record CreateBranchRequest : IRequest<ApiResponse<BranchDTO>>
{
	public string Sign { get; init; }
	public string Title { get; init; }
	
	public CreateBranchRequest(string sign, string title = "")
	{
		Sign = sign.ToUpper();
		Title = title.ToUpper();
	}
}
