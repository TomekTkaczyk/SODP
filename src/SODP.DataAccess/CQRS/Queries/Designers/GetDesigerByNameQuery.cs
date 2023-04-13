using Microsoft.EntityFrameworkCore;
using SODP.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.DataAccess.CQRS.Queries.Designers;

public class GetDesigerByNameQuery : QueryBase<Designer>
{
	private readonly string _firstName;
	private readonly string _lastName;

	public GetDesigerByNameQuery(string firstName, string lastName)
	{
		_firstName = firstName;
		_lastName = lastName;
	}

	public override async Task<Designer> ExecuteAsync(SODPDBContext context, CancellationToken cancellationToken)
	{
		return await context.Set<Designer>()
			.FirstOrDefaultAsync(x =>
			x.Firstname.ToUpper().Equals(_firstName.ToUpper()) &&
			x.Lastname.ToUpper().Equals(_lastName.ToUpper()),cancellationToken);
	}
}
