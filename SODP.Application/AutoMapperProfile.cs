using AutoMapper;
using SODP.Model;
using SODP.Shared.DTO;
using System.Linq;

namespace SODP.Domain
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.ActiveStatus, opt => opt.MapFrom(src => src.LockoutEnabled))
                .ForMember(dest => dest.Roles, act => act.Ignore())
                .ReverseMap();

            CreateMap<Role, RoleDTO>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Name));

            CreateMap<Stage, StageDTO>()
                .ReverseMap();

            CreateMap<ProjectBranch, ProjectBranchDTO>()
                .ForMember(dest => dest.Name, act => act.Ignore())
                .ForMember(dest => dest.DesignerId, act => act.Ignore())
                .ForMember(dest => dest.DesignerName, act => act.Ignore())
                .ForMember(dest => dest.CheckerId, act => act.Ignore())
                .ForMember(dest => dest.CheckerName, act => act.Ignore());

            CreateMap<Project, ProjectDTO>()
                .ForMember(dest => dest.Branches, opt => opt.MapFrom(src => src.Branches));


            CreateMap<Project, NewProjectDTO>()
                .ForMember(dest => dest.StageId, opt => opt.MapFrom(src => src.StageId));

            CreateMap<ProjectDTO, Project>()
                .ForMember(dest => dest.Location, act => act.Ignore())
                .ForMember(dest => dest.Branches, act => act.Ignore())
                .ForMember(dest => dest.CreateTimeStamp, act => act.Ignore())
                .ForMember(dest => dest.ModifyTimeStamp, act => act.Ignore())
                .ForMember(dest => dest.Stage, act => act.Ignore())
                .PreserveReferences();

            CreateMap<NewProjectDTO, Project>()
                .ForMember(dest => dest.Location, act => act.Ignore())
                .ForMember(dest => dest.Status, act => act.Ignore())
                .ForMember(dest => dest.Branches, act => act.Ignore())
                .ForMember(dest => dest.Stage, act => act.Ignore())
                .ForMember(dest => dest.CreateTimeStamp, act => act.Ignore())
                .ForMember(dest => dest.ModifyTimeStamp, act => act.Ignore())
                .PreserveReferences();

            CreateMap<BranchDTO, Branch>()
                .ForMember(dest => dest.Licenses, act => act.Ignore())
                .ForMember(dest => dest.CreateTimeStamp, act => act.Ignore())
                .ForMember(dest => dest.ModifyTimeStamp, act => act.Ignore())
                .PreserveReferences();

            CreateMap<Branch, BranchDTO>()
                .PreserveReferences();

            CreateMap<Designer, DesignerDTO>()
                .PreserveReferences();

            CreateMap<Licence, LicenceDTO>()
                .ForMember(dest => dest.Branches, opt => opt.MapFrom(x => x.Branches.Select(y => y.Branch).ToList()));
        }
    }
}
