using SODP.Domain.Entities;
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
			context.Add(_designer);
			await context.SaveChangesAsync();

			return _designer;
		}
	}
}
