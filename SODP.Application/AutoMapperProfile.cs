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


			CreateMap<AppDictionary, DictionaryDTO>()
				.ReverseMap();


			CreateMap<Stage, StageDTO>()
                .ReverseMap();


            CreateMap<Investor, InvestorDTO>()
                .ReverseMap();


            CreateMap<Project, ProjectDTO>();


            CreateMap<PartBranch, PartBranchDTO>()
                .ForMember(dest => dest.Part, act => act.Ignore())
                .ForMember(dest => dest.Roles, act => act.Ignore());

            CreateMap<PartBranchDTO, PartBranch>()
                .ForMember(dest => dest.ProjectPartId, act => act.Ignore())
                .ForMember(dest => dest.ProjectPart, act => act.Ignore())
                .ForMember(dest => dest.BranchId, act => act.Ignore())
                .ForMember(dest => dest.Branch, act => act.Ignore())
                .ForMember(dest => dest.CreateTimeStamp, act => act.Ignore())
                .ForMember(dest => dest.ModifyTimeStamp, act => act.Ignore())
                .ForMember(dest => dest.Roles, act => act.Ignore());


            CreateMap<ProjectPart, ProjectPartDTO>()
                .ForMember(dest => dest.Branches, opt => opt.MapFrom(src => src.Branches))
                .ForMember(dest => dest.Part, act => act.Ignore())
                .PreserveReferences();

            CreateMap<ProjectPartDTO, ProjectPart>()
                .ForMember(dest => dest.Branches, opt => opt.MapFrom(src => src.Branches))
                .ForMember(dest => dest.Sign, act => act.Ignore())
                .ForMember(dest => dest.Name, act => act.Ignore())
                .ForMember(dest => dest.CreateTimeStamp, act => act.Ignore())
                .ForMember(dest => dest.ModifyTimeStamp, act => act.Ignore())
                .PreserveReferences();


            CreateMap<Branch, BranchDTO>()
               .ForMember(dest => dest.Sign, opt => opt.MapFrom(x => x.Sign.ToUpper()))
               .ForMember(dest => dest.Name, opt => opt.MapFrom(x => x.Name.ToUpper()))
               .ForMember(dest => dest.Order, act => act.Ignore())
               .PreserveReferences();


            CreateMap<ProjectBranchRole, ProjectBranchRoleDTO>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Role.ToString()))
                .PreserveReferences();


            CreateMap<Project, NewProjectDTO>()
                .ForMember(dest => dest.StageId, opt => opt.MapFrom(src => src.StageId));


            CreateMap<ProjectDTO, Project>()
                .AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s)
                .ForMember(dest => dest.Parts, act => act.Ignore())
                .ForMember(dest => dest.CreateTimeStamp, act => act.Ignore())
                .ForMember(dest => dest.ModifyTimeStamp, act => act.Ignore())
                .ForMember(dest => dest.Stage, act => act.Ignore())
                .PreserveReferences();


            CreateMap<NewProjectDTO, Project>()
                .AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s)
                .ForMember(dest => dest.Status, act => act.Ignore())
                .ForMember(dest => dest.Parts, act => act.Ignore())
                .ForMember(dest => dest.Stage, act => act.Ignore())
                .ForMember(dest => dest.CreateTimeStamp, act => act.Ignore())
                .ForMember(dest => dest.ModifyTimeStamp, act => act.Ignore())
                .ForMember(dest => dest.Address, act => act.Ignore())
                .ForMember(dest => dest.Investor, act => act.Ignore())
                .ForMember(dest => dest.Investor, act => act.Ignore())
                .ForMember(dest => dest.Title, act => act.Ignore())
                .ForMember(dest => dest.LocationUnit, act => act.Ignore())
                .ForMember(dest => dest.BuildingCategory, act => act.Ignore())
                .ForMember(dest => dest.BuildingPermit, act => act.Ignore())
                .ForMember(dest => dest.Description, act => act.Ignore())
                .ForMember(dest => dest.DevelopmentDate, act => act.Ignore())
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
                .AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s)
                .ReverseMap();


            CreateMap<NewLicenseDTO, License>()
                .ForMember(dest => dest.Id, act => act.Ignore())
                .ForMember(dest => dest.Designer, act => act.Ignore())
                .ForMember(dest => dest.Branches, act => act.Ignore())
                .ForMember(dest => dest.CreateTimeStamp, act => act.Ignore())
                .ForMember(dest => dest.ModifyTimeStamp, act => act.Ignore())
                .AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s)
                .PreserveReferences();
        }
    }
}
