using AutoMapper;
using SODP.Shared.DTO;
using System;

namespace SODP.UI.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ProjectPartDTO, Pages.ActiveProjects.ViewModels.PartVM>()
                .ForMember(dest => dest.Sign, opt => opt.MapFrom(x => x.Sign))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name));

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

			CreateMap<ProjectBranchRoleDTO, Pages.ActiveProjects.ViewModels.BranchRoleVM>()
                .ForMember(dest => dest.Designer, opt => opt.MapFrom(x => x.License.Designer.ToString()))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(x => x.License.Content));

			CreateMap<ProjectBranchRoleDTO, Pages.ArchiveProjects.ViewModels.RoleVM>()
				.ForMember(dest => dest.Designer, opt => opt.MapFrom(x => x.License.Designer.ToString()))
				.ForMember(dest => dest.Content, opt => opt.MapFrom(x => x.License.Content));

			CreateMap<BranchDTO, Pages.ActiveProjects.ViewModels.BranchVM>();

			CreateMap<BranchDTO, Pages.ArchiveProjects.ViewModels.BranchVM>();

            CreateMap<InvestorDTO, Pages.ActiveProjects.ViewModels.InvestorVM>()
                .ReverseMap();

            CreateMap<LicenseDTO, Pages.ActiveProjects.ViewModels.LicenseVM>()
                .ForMember(dest => dest.Designer, opt => opt.MapFrom(x => x.Designer.ToString()));

            CreateMap<PartDTO, Pages.ActiveProjects.ViewModels.PartVM>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Sign, opt => opt.MapFrom(x => x.Sign))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name));

            CreateMap<PartDTO, Pages.ActiveProjects.ViewModels.NewPartVM>()
                .ForMember(dest => dest.Sign, opt => opt.MapFrom(x => x.Sign))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name));

			CreateMap<PartDTO, Pages.Parts.ViewModels.PartVM>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
				.ForMember(dest => dest.Sign, opt => opt.MapFrom(x => x.Sign))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name));

			CreateMap<PartDTO, Pages.Parts.ViewModels.NewPartVM>()
				.ForMember(dest => dest.Sign, opt => opt.MapFrom(x => x.Sign))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name));

			CreateMap<BranchRoleDTO, Pages.ActiveProjects.ViewModels.BranchRoleVM>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(x => x.Role))
                .ForMember(dest => dest.Designer, opt => opt.MapFrom(x => x.License.Designer.ToString()))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(x => x.License.Content));

            CreateMap<PartBranchDTO, Pages.ActiveProjects.ViewModels.PartBranchVM>();

            CreateMap<ProjectPartDTO, Pages.ActiveProjects.ViewModels.ProjectPartVM>();


		}
	}
}
