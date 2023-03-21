using SODP.Shared.DTO;
using SODP.UI.Pages.ActiveProjects.ViewModels;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SODP.UI.Extensions
{
	public static class AvailableBranchesVMExtensions
	{
		public static StringContent ToHttpContent(this AvailableBranchesVM project)
		{
			return new StringContent(
								  JsonSerializer.Serialize(project),
								  Encoding.UTF8,
								  "application/json"
							  );
		}

	}
}
