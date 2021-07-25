using AutoMapper;
using SODP.Model;
using SODP.Shared.DTO;

namespace SODP.Domain
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.ActiveStatus, opt => opt.MapFrom(src => src.LockoutEnabled))
                .ReverseMap();

            CreateMap<UserDTO, UserUpdateDTO>();

            CreateMap<Role, RoleDTO>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Name));

            CreateMap<Stage, StageDTO>()
                .ReverseMap();

            CreateMap<Project, ProjectDTO>()
                .ForMember(dest => dest.Branches, act => act.Ignore());

            CreateMap<ProjectDTO, Project>()
                .ForMember(dest => dest.Stage, act => act.Ignore());

            CreateMap<BranchDTO, Branch>()
                .ForMember(dest => dest.CreateTimeStamp, act => act.Ignore())
                .ForMember(dest => dest.ModifyTimeStamp, act => act.Ignore());

            CreateMap<Branch, BranchDTO>();

        }
    }
}
