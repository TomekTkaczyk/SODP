namespace SODP.Domain.Entities
{
	public class ActivatedEntity : BaseEntity, IActiveStatus
	{
		public bool ActiveStatus { get; set; }

		public void SetActiveStatus(bool activeStatus)
		{
			ActiveStatus = activeStatus;
		}
	}
}
