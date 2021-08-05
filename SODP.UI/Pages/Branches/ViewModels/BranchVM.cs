using SODP.Shared.DTO;
using SODP.UI.Mappers;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace SODP.UI.Pages.Branches.ViewModels
{
    public class BranchVM : NewBranchVM
    {
        public int Id { get; set; }
    }
}
