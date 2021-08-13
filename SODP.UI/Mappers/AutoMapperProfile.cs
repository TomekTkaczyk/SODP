using AutoMapper;
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
                .PreserveReferences();
        }
    }
}
