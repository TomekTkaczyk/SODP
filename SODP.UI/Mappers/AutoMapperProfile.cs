using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using SODP.Shared.DTO;
using SODP.UI.Pages.ActiveProjects.ViewModels;

namespace SODP.UI.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProjectDTO, ProjectVM>()
                .AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s)
                .ForMember(dest => dest.Stage, act => act.Ignore())
                .ForMember(dest => dest.StageId, opt => opt.MapFrom(x => x.Stage.Id))
                .ForMember(dest => dest.StageSign, opt => opt.MapFrom(x => x.Stage.Sign))
                .ForMember(dest => dest.StageName, opt => opt.MapFrom(x => x.Stage.Name))
                .PreserveReferences();

            CreateMap<ProjectBranchDTO, ProjectBranchVM>();

            CreateMap<ProjectBranchRoleDTO, RoleVM>()
                .ForMember(dest => dest.Designer, opt => opt.MapFrom(x => x.License.Designer.ToString()))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(x => x.License.Content));

            CreateMap<BranchDTO, BranchVM>();


            CreateMap<ProjectBranchDTO, SelectListItem>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(x => $"{x.Branch.Name}"))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(x => $"{x.Branch.Id}"))
                .PreserveReferences();
        }
    }
}
