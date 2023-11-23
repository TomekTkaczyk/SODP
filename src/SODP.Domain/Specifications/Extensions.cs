using System.Linq;

namespace SODP.Domain.Specifications;

public static class Extensions
{
	public static OrSpecification<T> Or<T>(this Specification<T> left, Specification<T> right)
	{
		return new OrSpecification<T>(left, right);
	}

	public static AndSpecification<T> And<T>(this Specification<T> left, Specification<T> right)
	{
		return new AndSpecification<T>(left, right);
	}

	public static IQueryable<T> GetQuery<T>(IQueryable<T> inputQueryable, Specification<T> specyfication)
	{
		return inputQueryable.AsQueryable().Where(specyfication);
	} 
}
