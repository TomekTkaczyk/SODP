using AutoMapper;
using SODP.Shared.DTO;
using System;

namespace SODP.UI.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            #region DTO to Pages/Shared/ViewModels

            CreateMap<ProjectPartDTO, Pages.Shared.ViewModels.PartVM>()
                .ForMember(dest => dest.Sign, opt => opt.MapFrom(x => x.Sign))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name));

            CreateMap<ProjectDTO, Pages.Shared.ViewModels.ProjectVM>()
                .AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s)
                .ForMember(dest => dest.Stage, act => act.Ignore())
                .ForMember(dest => dest.StageId, opt => opt.MapFrom(x => x.Stage.Id))
                .ForMember(dest => dest.StageSign, opt => opt.MapFrom(x => x.Stage.Sign))
                .ForMember(dest => dest.StageName, opt => opt.MapFrom(x => x.Stage.Name))
                .ForMember(dest => dest.DevelopmentDate, opt => opt.MapFrom(x => x.DevelopmentDate == null ? null : ((DateTime)x.DevelopmentDate).Date.ToShortDateString()))
                .PreserveReferences();

			CreateMap<BranchDTO, Pages.Shared.ViewModels.BranchVM>();

            CreateMap<InvestorDTO, Pages.Shared.ViewModels.InvestorVM>();

            CreateMap<LicenseDTO, Pages.Shared.ViewModels.LicenseVM>()
                .ForMember(dest => dest.Designer, opt => opt.MapFrom(x => x.Designer.ToString()));

            CreateMap<PartDTO, Pages.Shared.ViewModels.PartVM>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
                .ForMember(dest => dest.Sign, opt => opt.MapFrom(x => x.Sign))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name));

			CreateMap<PartDTO, Pages.Parts.ViewModels.PartVM>()
				.ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.Id))
				.ForMember(dest => dest.Sign, opt => opt.MapFrom(x => x.Sign))
				.ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name));

			CreateMap<BranchRoleDTO, Pages.Shared.ViewModels.BranchRoleVM>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(x => x.Role))
                .ForMember(dest => dest.Designer, opt => opt.MapFrom(x => x.License.Designer.ToString()))
                .ForMember(dest => dest.Content, opt => opt.MapFrom(x => x.License.Content));

            CreateMap<PartBranchDTO, Pages.Shared.ViewModels.PartBranchVM>();

            CreateMap<ProjectPartDTO, Pages.Shared.ViewModels.ProjectPartVM>();

            #endregion

            #region DTO to Pages/Stages/ViewModels

            CreateMap<StageDTO, Pages.Stages.ViewModels.StageVM>();
            
            #endregion

        }
    }
}
