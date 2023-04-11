using System.Threading.Tasks;

namespace SODP.Application.Abstractions;

public interface IQueryBase<TResult>
{
	public TResult Result { get; set; }

	Task<TResult> ExecuteAsync();
}
