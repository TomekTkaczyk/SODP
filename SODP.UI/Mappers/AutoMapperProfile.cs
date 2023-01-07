using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using SODP.Shared.DTO;
using System;

namespace SODP.UI.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProjectBranchDTO, Pages.ActiveProjects.ViewModels.ProjectBranchVM>()
                .ForMember(dest => dest.Branch, act => act.Ignore())
                .ForMember(dest => dest.Roles, act => act.Ignore());

            CreateMap<Pages.ActiveProjects.ViewModels.ProjectBranchVM, ProjectBranchDTO>()
                .ForMember(dest => dest.Roles, act => act.Ignore())
                .ForMember(dest => dest.Part, act => act.Ignore());


            CreateMap<ProjectBranchDTO, Pages.ArchiveProjects.ViewModels.ProjectBranchVM>()
                .ForMember(dest => dest.Branch, act => act.Ignore())
                .ForMember(dest => dest.Roles, act => act.Ignore());


            CreateMap<ProjectDTO, Pages.ActiveProjects.ViewModels.ProjectVM>()
                .AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s)
                .ForMember(dest => dest.Stage, act => act.Ignore())
                .ForMember(dest => dest.StageId, opt => opt.MapFrom(x => x.Stage.Id))
                .ForMember(dest => dest.StageSign, opt => opt.MapFrom(x => x.Stage.Sign))
                .ForMember(dest => dest.StageName, opt => opt.MapFrom(x => x.Stage.Name))
                .ForMember(dest => dest.DevelopmentDate, opt => opt.MapFrom(x => x.DevelopmentDate == null ? null : ((DateTime)x.DevelopmentDate).Date.ToShortDateString()))
                .ForMember(dest => dest.ProjectBranches, opt => opt.MapFrom(x => new Pages.ActiveProjects.ViewModels.BranchesVM()))
                .ForMember(dest => dest.AvailableBranches, act => act.Ignore())
                .PreserveReferences();

			CreateMap<ProjectDTO, Pages.ArchiveProjects.ViewModels.ProjectVM>()
	            .AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s)
	            .ForMember(dest => dest.Stage, act => act.Ignore())
	            .ForMember(dest => dest.StageId, opt => opt.MapFrom(x => x.Stage.Id))
	            .ForMember(dest => dest.StageSign, opt => opt.MapFrom(x => x.Stage.Sign))
	            .ForMember(dest => dest.StageName, opt => opt.MapFrom(x => x.Stage.Name))
                .ForMember(dest => dest.DevelopmentDate, opt => opt.MapFrom(x => x.DevelopmentDate == null ? null : ((DateTime)x.DevelopmentDate).Date.ToShortDateString()))
				.ForMember(dest => dest.ProjectBranches, opt => opt.MapFrom(x => new Pages.ArchiveProjects.ViewModels.BranchesVM()))
                .ForMember(dest => dest.AvailableBranches, act => act.Ignore())
                .PreserveReferences();

			CreateMap<ProjectBranchRoleDTO, Pages.ActiveProjects.ViewModels.RoleVM>()
                .ForMember(dest => dest.Designer, opt => opt.MapFrom(x => x.License.Designer.ToString()))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(x => x.License.Content));

			CreateMap<ProjectBranchRoleDTO, Pages.ArchiveProjects.ViewModels.RoleVM>()
				.ForMember(dest => dest.Designer, opt => opt.MapFrom(x => x.License.Designer.ToString()))
				.ForMember(dest => dest.Content, opt => opt.MapFrom(x => x.License.Content));

			CreateMap<BranchDTO, Pages.ActiveProjects.ViewModels.BranchVM>();

			CreateMap<BranchDTO, Pages.ArchiveProjects.ViewModels.BranchVM>();

            CreateMap<InvestorDTO, Pages.ActiveProjects.ViewModels.InvestorVM>()
                .ReverseMap();

			CreateMap<ProjectBranchDTO, SelectListItem>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(x => $"{x.Part.Name}"))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(x => $"{x.Part.Id}"))
                .ForMember(dest => dest.Disabled, act => act.Ignore())
                .ForMember(dest => dest.Group, act => act.Ignore())
                .ForMember(dest => dest.Selected, act => act.Ignore())
                .PreserveReferences();

            CreateMap<LicenseDTO, Pages.ActiveProjects.ViewModels.LicenseVM>()
                .ForMember(dest => dest.Designer, opt => opt.MapFrom(x => x.Designer.ToString()));
        }
    }
}
