using SODP.DataAccess.CQRS.Queries.Designers;
using SODP.Domain.Entities;
using SODP.Domain.Exceptions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SODP.DataAccess.CQRS.Commands.Designers
{
	public class CreateDesignerCommand : CommandBase<Designer, Designer>
	{
		private readonly Designer _designer;

		public CreateDesignerCommand(Designer designer)
        {
			_designer = designer;
		}

        public override async Task<Designer> ExecuteAsync(SODPDBContext context, CancellationToken cancellationToken)
		{
			var query = new GetDesigerByNameQuery(_designer.Firstname, _designer.Lastname);
			var existDesigner = await query.ExecuteAsync(context, cancellationToken);
			if (existDesigner is not null)
			{
				throw new ConflictException(nameof(Designer));
			}

			context.Add(_designer);
			await context.SaveChangesAsync();

			return _designer;
		}
	}
}
