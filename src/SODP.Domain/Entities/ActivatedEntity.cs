namespace SODP.Domain.Entities
{
	public abstract class ActivatedEntity : BaseEntity, IActiveStatus
	{
		public bool ActiveStatus { get; private set; }

		public void SetActiveStatus(bool activeStatus)
		{
			ActiveStatus = activeStatus;
		}
	}
}
