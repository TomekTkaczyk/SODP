using SODP.DataAccess;
using SODP.Domain.Entities;
using SODP.Domain.Repositories;

namespace SODP.Infrastructure.Repositories;

public class AppDictionaryRepository : PagedRepository<AppDictionary>, IAppDictionaryRepository
{
	public AppDictionaryRepository(SODPDBContext dbContext) : base(dbContext) { }
}
