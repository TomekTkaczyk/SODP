using AutoMapper;
//using SODP.Domain.DTO;
using SODP.Model;
using SODP.Shared.DTO;
using System.Collections.Generic;

namespace SODP.Domain
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, UserUpdateDTO>();

            CreateMap<Role, RoleDTO>().ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Name));

            CreateMap<Stage, StageDTO>().ReverseMap();

            CreateMap<Project, ProjectDTO>();
            CreateMap<ProjectDTO, Project>().ForMember(dest => dest.Stage, act => act.Ignore());

        }
    }
}
