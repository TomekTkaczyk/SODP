using SODP.Domain.Entities;
using SODP.Domain.Shared.Specifications;
using SODP.Shared.Enums;
using System;
using System.Linq.Expressions;

namespace SODP.Infrastructure.Specifications.Projects;

public class ProjectByNameSpecyfication : Specification<Project>
{
	public ProjectByNameSpecyfication(ProjectStatus status, string searchString)
		: base(project =>
		   (project.Status == status)
		   &&
		   (
				string.IsNullOrWhiteSpace(searchString)
			 || project.Number.Value.Contains(searchString)
		   ))
    {
		AddInclude(x => x.Stage);
		AddOrderByExpression(x => x.Symbol);
	}

 //   public override Expression<Func<Project, bool>> AsPredicateExpression()
	//{
	//	return project =>
	//	   (project.Status == _status)
	//	   &&
	//	   (
	//			string.IsNullOrWhiteSpace(_searchString)
	//		 || project.Number.Value.Contains(_searchString)
	//	   ); 
	//}
}

//public class ProjectByNameSpecyfication : Specification<Project>
//{
//    public ProjectByNameSpecyfication(ProjectStatus status, string searchString)
//        : base(project =>
//        (project.Status == status) &&
//            (string.IsNullOrWhiteSpace(searchString)
//             || project.Name.Contains(searchString)
//             || project.Number.Value.Contains(searchString)
//             //|| project.Description.Contains(searchString)
//             //|| project.Investor.Contains(searchString)
//             //|| project.Address.Contains(searchString)
//            )
//        )
//    {
//        AddInclude(i => i.Stage);

//        AddOrderBy(x => x.Number, false);
//        AddOrderBy(x => x.Stage.Sign, false);
//    }
//}