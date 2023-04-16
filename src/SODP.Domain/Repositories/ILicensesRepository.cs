using SODP.Domain.Entities;
using System.Collections.Generic;

namespace SODP.Domain.Repositories;

public interface ILicensesRepository : IRepository<License>
{
	IEnumerable<License> GetAll(int designerId);
}
