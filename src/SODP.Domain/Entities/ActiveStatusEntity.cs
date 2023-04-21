namespace SODP.Domain.Entities;

public abstract class ActiveStatusEntity : BaseEntity, IActiveStatus
{
	public bool ActiveStatus { get; set; }

	public void SetActiveStatus(bool activeStatus)
	{
		ActiveStatus = activeStatus;
	}
}
