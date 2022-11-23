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



            CreateMap<Project, ProjectDTO>();

            CreateMap<ProjectBranch, ProjectBranchDTO>()
                .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.Branch))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles))
                .PreserveReferences();

            CreateMap<ProjectBranchRole, ProjectBranchRoleDTO>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()))
                .PreserveReferences();


            CreateMap<Branch, BranchDTO>()
               .ForMember(dest => dest.Sign, opt => opt.MapFrom(x => x.Sign.ToUpper()))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name.ToUpper()))
               .ForMember(dest => dest.Order, act => act.Ignore())
               .ForMember(dest => dest.ActiveStatus, act => act.Ignore())
               .PreserveReferences();




            CreateMap<Project, NewProjectDTO>()
                .ForMember(dest => dest.StageId, opt => opt.MapFrom(src => src.StageId));

            CreateMap<ProjectDTO, Project>()
                .AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s)
                .ForMember(dest => dest.Branches, act => act.Ignore())
                .ForMember(dest => dest.CreateTimeStamp, act => act.Ignore())
                .ForMember(dest => dest.ModifyTimeStamp, act => act.Ignore())
                .ForMember(dest => dest.Stage, act => act.Ignore())
                .PreserveReferences();

            CreateMap<NewProjectDTO, Project>()
                .AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s)
                .ForMember(dest => dest.Status, act => act.Ignore())
                .ForMember(dest => dest.Branches, act => act.Ignore())
                .ForMember(dest => dest.Stage, act => act.Ignore())
                .ForMember(dest => dest.CreateTimeStamp, act => act.Ignore())
                .ForMember(dest => dest.ModifyTimeStamp, act => act.Ignore())
                .ForMember(dest => dest.Address, act => act.Ignore())
                .ForMember(dest => dest.Investor, act => act.Ignore())
                .ForMember(dest => dest.Investor, act => act.Ignore())

                .PreserveReferences();

            CreateMap<BranchDTO, Branch>()
                .ForMember(dest => dest.Sign, opt => opt.MapFrom(x => x.Sign.ToUpper()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name.ToUpper()))
                .ForMember(dest => dest.Licenses, act => act.Ignore())
                .ForMember(dest => dest.CreateTimeStamp, act => act.Ignore())
                .ForMember(dest => dest.ModifyTimeStamp, act => act.Ignore());



            CreateMap<Designer, DesignerDTO>()
                .AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s)
                .ReverseMap()
                .PreserveReferences();

            CreateMap<License, LicenseWithBranchesDTO>()
                .ForMember(dest => dest.Designer, opt => opt.MapFrom(x => x.Designer))
                .ForMember(dest => dest.Branches, opt => opt.MapFrom(x => x.Branches.Select(y => y.Branch)))
                .PreserveReferences();

            CreateMap<License, LicenseDTO>()
                .AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s);

            CreateMap<NewLicenseDTO, License>()
                .AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s)
                .PreserveReferences();
        }
    }
}
