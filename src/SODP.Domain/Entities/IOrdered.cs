namespace SODP.Domain.Entities
{
	public interface IOrdered
	{
		int Order { get; }

		public void Up();

		public void Down();
	}
}
