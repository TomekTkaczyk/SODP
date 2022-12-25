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
			CreateMap<ProjectBranchDTO, Pages.ActiveProjects.ViewModels.ProjectBranchVM>();
			
            CreateMap<ProjectBranchDTO, Pages.ArchiveProjects.ViewModels.ProjectBranchVM>();

            CreateMap<ProjectDTO, Pages.ActiveProjects.ViewModels.ProjectVM>()
                .AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s)
                .ForMember(dest => dest.Stage, act => act.Ignore())
                .ForMember(dest => dest.StageId, opt => opt.MapFrom(x => x.Stage.Id))
                .ForMember(dest => dest.StageSign, opt => opt.MapFrom(x => x.Stage.Sign))
                .ForMember(dest => dest.StageName, opt => opt.MapFrom(x => x.Stage.Name))
                .ForMember(dest => dest.DevelopmentDate, opt => opt.MapFrom(x => x.DevelopmentDate == null ? null : ((DateTime)x.DevelopmentDate).Date.ToShortDateString()))
                .ForMember(dest => dest.ProjectBranches, opt => opt.MapFrom(x => new Pages.ActiveProjects.ViewModels.BranchesVM()))
                .ForPath(dest => dest.ProjectBranches.Branches, opt => opt.MapFrom(x => x.Branches))
                .PreserveReferences();

			CreateMap<ProjectDTO, Pages.ArchiveProjects.ViewModels.ProjectVM>()
	            .AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s)
	            .ForMember(dest => dest.Stage, act => act.Ignore())
	            .ForMember(dest => dest.StageId, opt => opt.MapFrom(x => x.Stage.Id))
	            .ForMember(dest => dest.StageSign, opt => opt.MapFrom(x => x.Stage.Sign))
	            .ForMember(dest => dest.StageName, opt => opt.MapFrom(x => x.Stage.Name))
                .ForMember(dest => dest.DevelopmentDate, opt => opt.MapFrom(x => x.DevelopmentDate == null ? null : ((DateTime)x.DevelopmentDate).Date.ToShortDateString()))
				.ForMember(dest => dest.ProjectBranches, opt => opt.MapFrom(x => new Pages.ArchiveProjects.ViewModels.BranchesVM()))
	            .ForPath(dest => dest.ProjectBranches.Branches, opt => opt.MapFrom(x => x.Branches))
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
                .ForMember(dest => dest.Text, opt => opt.MapFrom(x => $"{x.Branch.Name}"))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(x => $"{x.Branch.Id}"))
                .PreserveReferences();

            CreateMap<LicenseDTO, Pages.ActiveProjects.ViewModels.LicenseVM>()
                .ForMember(dest => dest.Designer, opt => opt.MapFrom(x => x.Designer.ToString()));
        }
    }
}
