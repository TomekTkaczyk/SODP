using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SODP.Domain.Services;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using SODP.UI.Pages.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SODP.UI.Pages.ActiveProjects
{
    [Authorize(Roles = "Administrator,ProjectManager")]
    public class EditModel : SODPPageModel
    {
        private readonly IStageService _stagesService;
        private readonly IProjectService _projectsService;

        public EditModel(IProjectService projectsService, IStageService stagesService)
        {
            _projectsService = projectsService;
            _stagesService = stagesService;
        }

        [BindProperty]
        public ProjectDTO Project { get; set; }

        public IEnumerable<SelectListItem> Stages { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                var stagesResponse = await _stagesService.GetAllAsync();
                Stages = stagesResponse.Data.Collection.Select(x => new SelectListItem()
                {
                    Value = x.Id.ToString(),
                    Text = $"({x.Sign.Trim()}) {x.Title.Trim()}"
                }).ToList();

                Project = new ProjectDTO();

                return Page();
            }

            var response = await _projectsService.GetAsync((int)id);
            if (response == null)
            {
                return NotFound();
            }
            Project = response.Data;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var stagesResponse = await _stagesService.GetAllAsync();
            Stages = stagesResponse.Data.Collection.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = $"({x.Sign.Trim()}) {x.Title.Trim()}"
            }).ToList();

            if (ModelState.IsValid)
            {
                ServiceResponse response = await _projectsService.UpdateAsync(Project);

                //if (Project.Id.Equals(0))
                //{
                //    var project = new ProjectDTO
                //    {
                //        Number = Project.Number,
                //        Title = Project.Title,
                //        Description = Project.Description,
                //        Stage = new StageDTO
                //        {

                //        }
                //    };
                //    response = await _projectsService.CreateAsync(project);
                //}
                //else
                //{
                //    var project = new ProjectDTO
                //    {
                //        Id = Project.Id,
                //        Number = Project.Number,
                //        StageId = Project.StageId,
                //        Title = Project.Title,
                //        Description = Project.Description
                //    };
                //    response = await _projectsService.UpdateAsync(project);
                //}

                if (!response.Success)
                {
                    return Page();
                }

                return RedirectToPage("Index");
            }
          
            return Page();
        }
    }
}
