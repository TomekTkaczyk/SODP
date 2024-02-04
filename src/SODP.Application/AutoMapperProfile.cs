using AutoMapper;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SODP.Domain.Entities;
using SODP.Domain.ValueObjects;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SODP.Domain;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap(typeof(Page<>), typeof(Page<>)).ConvertUsing(typeof(PageConverter<,>));


        CreateMap<User, UserDTO>()
			.ForMember(dest => dest.Firstname, opt => opt.MapFrom(x => x.Firstname.Value))
			.ForMember(dest => dest.Lastname, opt => opt.MapFrom(x => x.Lastname.Value))
			.ForMember(dest => dest.ActiveStatus, opt => opt.MapFrom(src => src.LockoutEnabled))
            .ForMember(dest => dest.Roles, act => act.Ignore())
            .ReverseMap()
            .PreserveReferences();


		CreateMap<Role, RoleDTO>()
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Name));

		CreateMap<Investor, InvestorDTO>()
            .ReverseMap();

		#region Dictionary

		CreateMap<AppDictionary, DictionaryDTO>()
                .ReverseMap();

        CreateMap<DictionaryDTO, AppDictionary>()
            .ForMember(dest => dest.CreateTimeStamp, act => act.Ignore())
            .ForMember(dest => dest.CreatedBy, act => act.Ignore())
            .ForMember(dest => dest.CreatedById, act => act.Ignore())
			.ForMember(dest => dest.ModifyTimeStamp, act => act.Ignore())
			.ForMember(dest => dest.ModifiedBy, act => act.Ignore())
			.ForMember(dest => dest.ModifiedById, act => act.Ignore())
			.ForMember(dest => dest.Parent, act => act.Ignore())
            .ReverseMap();

		#endregion

        #region Stage

        CreateMap<Stage, StageDTO>()
            .ForMember(dest => dest.Sign, opt => opt.MapFrom(x => x.Sign.Value))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(x => x.Title.Value))
            .PreserveReferences();

        CreateMap<StageDTO, Stage>()
            .ForMember(dest => dest.Order, act => act.Ignore())
            .ForMember(dest => dest.CreateTimeStamp, act => act.Ignore())
            .ForMember(dest => dest.CreatedBy, act => act.Ignore())
            .ForMember(dest => dest.CreatedById, act => act.Ignore())
            .ForMember(dest => dest.ModifyTimeStamp, act => act.Ignore())
            .ForMember(dest => dest.ModifiedBy, act => act.Ignore())
            .ForMember(dest => dest.ModifiedById, act => act.Ignore());

        #endregion

        #region Part

        CreateMap<Part, PartDTO>()
            .ForMember(dest => dest.Sign, opt => opt.MapFrom(x => x.Sign.Value))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(x => x.Title.Value))
            .PreserveReferences();

        CreateMap<PartDTO, Part>()
           .ForMember(dest => dest.CreateTimeStamp, act => act.Ignore())
           .ForMember(dest => dest.CreatedBy, act => act.Ignore())
           .ForMember(dest => dest.CreatedById, act => act.Ignore())
           .ForMember(dest => dest.ModifyTimeStamp, act => act.Ignore())
           .ForMember(dest => dest.ModifiedBy, act => act.Ignore())
           .ForMember(dest => dest.ModifiedById, act => act.Ignore());

        #endregion

        #region BranchRole

        CreateMap<BranchRole, BranchRoleDTO>();

        CreateMap<BranchRoleDTO, BranchRole>()
			.ForMember(dest => dest.PartBranch, act => act.Ignore())
			.ForMember(dest => dest.PartBranchId, act => act.Ignore())
			.ForMember(dest => dest.CreateTimeStamp, act => act.Ignore())
            .ForMember(dest => dest.CreatedBy, act => act.Ignore())
            .ForMember(dest => dest.CreatedById, act => act.Ignore())
            .ForMember(dest => dest.ModifyTimeStamp, act => act.Ignore())
            .ForMember(dest => dest.ModifiedBy, act => act.Ignore())
            .ForMember(dest => dest.ModifiedById, act => act.Ignore());

		#endregion

		#region PartBranch

		CreateMap<PartBranchDTO, PartBranch>()
            .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.Branch))
            .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles))
            .ForMember(dest => dest.ProjectPartId, act => act.Ignore())
            .ForMember(dest => dest.ProjectPart, act => act.Ignore())
            .ForMember(dest => dest.BranchId, act => act.Ignore())
            .ForMember(dest => dest.CreateTimeStamp, act => act.Ignore())
            .ForMember(dest => dest.CreatedBy, act => act.Ignore())
            .ForMember(dest => dest.CreatedById, act => act.Ignore())
            .ForMember(dest => dest.ModifyTimeStamp, act => act.Ignore())
            .ForMember(dest => dest.ModifiedBy, act => act.Ignore())
            .ForMember(dest => dest.ModifiedById, act => act.Ignore());

        CreateMap<PartBranch, PartBranchDTO>();

        #endregion

        #region ProjectPart

        CreateMap<ProjectPart, ProjectPartDTO>()
            .ForMember(dest => dest.Sign, opt => opt.MapFrom(x => x.Sign.Value))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(x => x.Title.Value))
            .ForMember(dest => dest.Branches, opt => opt.MapFrom(src => src.Branches))
            .PreserveReferences();

        CreateMap<ProjectPartDTO, ProjectPart>()
            .ForMember(dest => dest.Branches, opt => opt.MapFrom(src => src.Branches))
            .ForMember(dest => dest.ProjectId, act => act.Ignore())
            .ForMember(dest => dest.Project, act => act.Ignore())
            .ForMember(dest => dest.CreateTimeStamp, act => act.Ignore())
			.ForMember(dest => dest.CreatedBy, act => act.Ignore())
            .ForMember(dest => dest.CreatedById, act => act.Ignore())
            .ForMember(dest => dest.ModifyTimeStamp, act => act.Ignore())
            .ForMember(dest => dest.ModifiedBy, act => act.Ignore())
            .ForMember(dest => dest.ModifiedById, act => act.Ignore())
            .PreserveReferences();

        #endregion

        #region Branch

        CreateMap<Branch, BranchDTO>()
            .ForCtorParam(ctorParamName: "Id", m => m.MapFrom(dest => dest.Id))
            .ForCtorParam(ctorParamName: "Sign", m => m.MapFrom(dest => dest.Sign.Value))
            .ForCtorParam(ctorParamName: "Title", m => m.MapFrom(dest => dest.Title.Value))
            .ForCtorParam(ctorParamName: "Order", m => m.MapFrom(dest => dest.Order))
            .ForCtorParam(ctorParamName: "ActiveStatus", m => m.MapFrom(dest => dest.ActiveStatus))
            .ForCtorParam(ctorParamName: "Licenses", m => m.MapFrom(dest => dest.Licenses))
            .PreserveReferences();

        CreateMap<BranchDTO, Branch>()
            .ForMember(dest => dest.Licenses, act => act.Ignore())
            .ForMember(dest => dest.CreateTimeStamp, act => act.Ignore())
            .ForMember(dest => dest.CreatedBy, act => act.Ignore())
            .ForMember(dest => dest.CreatedById, act => act.Ignore())
            .ForMember(dest => dest.ModifyTimeStamp, act => act.Ignore())
            .ForMember(dest => dest.ModifiedBy, act => act.Ignore())
            .ForMember(dest => dest.ModifiedById, act => act.Ignore())
            .PreserveReferences();


		CreateMap<BranchLicense, LicenseDTO>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(x => x.License.Id))
            .ForMember(dest => dest.Designer, opt => opt.MapFrom(x => x.License.Designer))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(x => x.License.Content))
            .ForMember(dest => dest.Branches, act => act.Ignore())
            .PreserveReferences();

        #endregion

        #region Project

        CreateMap<Project, NewProjectDTO>()
                .ForMember(dest => dest.StageSign, opt => opt.MapFrom(src => src.Stage.Sign));

        CreateMap<NewProjectDTO, Project>()
            .AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s)
            .ForMember(dest => dest.Status, act => act.Ignore())
            .ForMember(dest => dest.Parts, act => act.Ignore())
            .ForMember(dest => dest.Stage, act => act.Ignore())
			.ForMember(dest => dest.StageId, act => act.Ignore())
			.ForMember(dest => dest.CreateTimeStamp, act => act.Ignore())
			.ForMember(dest => dest.CreatedBy, act => act.Ignore())
			.ForMember(dest => dest.CreatedById, act => act.Ignore())
			.ForMember(dest => dest.ModifyTimeStamp, act => act.Ignore())
			.ForMember(dest => dest.ModifiedBy, act => act.Ignore())
			.ForMember(dest => dest.ModifiedById, act => act.Ignore())
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

        CreateMap<Project, ProjectDTO>()
            .AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s)
            .ForMember(dest => dest.Number, act => act.MapFrom(x => x.Number.Value))
            .ForMember(dest => dest.Name, act => act.MapFrom(x => x.Name.Value))
            .ForMember(dest => dest.Title, act => act.MapFrom(x => x.Title.Value))
            .ForMember(dest => dest.Address, act => act.MapFrom(x => x.Address.Value));

        CreateMap<ProjectDTO, Project>()
            .AddTransform<string>(s => string.IsNullOrEmpty(s) ? "" : s)
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address ?? Address.Default))
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title ?? Title.Default))
            .ForMember(dest => dest.Parts, act => act.Ignore())
			.ForMember(dest => dest.Stage, act => act.Ignore())
			.ForMember(dest => dest.CreateTimeStamp, act => act.Ignore())
			.ForMember(dest => dest.CreatedBy, act => act.Ignore())
			.ForMember(dest => dest.CreatedById, act => act.Ignore())
			.ForMember(dest => dest.ModifyTimeStamp, act => act.Ignore())
			.ForMember(dest => dest.ModifiedBy, act => act.Ignore())
			.ForMember(dest => dest.ModifiedById, act => act.Ignore())
            .PreserveReferences();

        #endregion

        #region Designer

        CreateMap<Designer, DesignerDTO>()
            .ForCtorParam(ctorParamName:"Id", m => m.MapFrom(dest => dest.Id))
            .ForCtorParam(ctorParamName:"Title", m => m.MapFrom<string>(dest => dest.Title.Value))
            .ForCtorParam(ctorParamName: "Firstname", m => m.MapFrom<string>(dest => dest.Firstname.Value))
            .ForCtorParam(ctorParamName: "Lastname", m => m.MapFrom<string>(dest => dest.Lastname.Value))
            .ForCtorParam(ctorParamName: "ActiveStatus", m => m.MapFrom(dest => dest.ActiveStatus))
            .ForCtorParam(ctorParamName: "Licenses", m => m.MapFrom(dest => dest.Licenses))
            .PreserveReferences();
        #endregion

        #region License

        CreateMap<NewLicenseDTO, License>()
            .ForMember(dest => dest.Id, act => act.Ignore())
            .ForMember(dest => dest.Designer, act => act.Ignore())
            .ForMember(dest => dest.Branches, act => act.Ignore())
			.ForMember(dest => dest.CreateTimeStamp, act => act.Ignore())
			.ForMember(dest => dest.CreatedBy, act => act.Ignore())
			.ForMember(dest => dest.CreatedById, act => act.Ignore())
			.ForMember(dest => dest.ModifyTimeStamp, act => act.Ignore())
			.ForMember(dest => dest.ModifiedBy, act => act.Ignore())
			.ForMember(dest => dest.ModifiedById, act => act.Ignore())
			.AddTransform<string>(s => string.IsNullOrEmpty(s) ? string.Empty : s)
            .PreserveReferences();

        CreateMap<License, LicenseDTO>()
            .ForCtorParam(ctorParamName: "Id", m => m.MapFrom(dest => dest.Id))
            .ForCtorParam(ctorParamName: "Designer", m => m.MapFrom(dest => dest.Designer))
            .ForCtorParam(ctorParamName: "Branches", m => m.MapFrom(dest => dest.Branches))
            .PreserveReferences();

        #endregion
    }

    private class PageConverter<TSource, TDestination> : ITypeConverter<Page<TSource>, Page<TDestination>>
    {
        public Page<TDestination> Convert(
            Page<TSource> source,
            Page<TDestination> destination,
            ResolutionContext context)
        {
            return new Page<TDestination>(
                source.PageNumber,
                source.PageSize,
                source.TotalCount,
                context.Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source.Collection));
        }
    }
}
