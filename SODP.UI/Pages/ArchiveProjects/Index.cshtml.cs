using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Infrastructure;
using SODP.UI.Pages.Shared;
using SODP.UI.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SODP.UI.Pages.ArchiveProjects
{
    [Authorize(Roles = "User, Administrator, ProjectManager")]
    public class IndexModel : ProjectsPageModel
    {
        public IndexModel(IWebAPIProvider apiProvider, ILogger<IndexModel> logger, IMapper mapper) : base(apiProvider, logger, mapper)
        {
            ReturnUrl = "/ArchiveProjects";
            _endpoint = "archive-projects";
        }        
    }
}
