using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using SODP.Shared.DTO;
using SODP.UI.ViewModels;

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
                .ForMember(dest => dest.StageTitle, opt => opt.MapFrom(x => x.Stage.Title))
                .PreserveReferences();

            CreateMap<ProjectBranchDTO, SelectListItem>()
                .ForMember(dest => dest.Text, opt => opt.MapFrom(x => x.Name))
                .PreserveReferences();
        }
    }
}
