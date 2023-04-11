using SODP.Domain.Entities;
using SODP.Shared.Response;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.Domain.Repositories;

public interface IInvestorRepository : IPageRepository<Investor>
{
}
