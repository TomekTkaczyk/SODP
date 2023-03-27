using System;
using System.Runtime.ExceptionServices;

namespace SODP.Domain.Entities;

public abstract class BaseEntity : IEquatable<BaseEntity>
{
    public int Id { get; private set; }

    public DateTime CreateTimeStamp { get; private set; }
    
    public DateTime ModifyTimeStamp { get; private set; }

    protected BaseEntity()										 
    {
        CreateTimeStamp = DateTime.UtcNow;
        ModifyTimeStamp = CreateTimeStamp;
    }

    public void SetCreateTimeStamp(DateTime timeStamp)
    {
        CreateTimeStamp = timeStamp;
    }

	public void SetModifyTimeStamp(DateTime timeStamp)
	{
		ModifyTimeStamp = timeStamp;
	}

	public override int GetHashCode()
	{
		return Id.GetHashCode();
	}

	public static bool operator == (BaseEntity left, BaseEntity right)
    {
		if (left is null && right is null)
		{
			return true;
		}

        return left is not null && right is not null && left.Equals(right); 
    }

	public static bool operator != (BaseEntity left, BaseEntity right)
	{
		return !(left == right);
	}

	public override bool Equals(object obj)
	{
		if (obj is null)
		{
			return false;
		}

		if (obj.GetType() != GetType())
		{
			return false;
		}

		if (obj is not BaseEntity entity)
		{
			return false;
		}

		return entity.Id == Id;
	}

	public bool Equals(BaseEntity other)
	{
		if (other is null)
		{
			return false;
		}

		if (other.GetType() != GetType())
		{
			return false;
		}

		return other.Id == Id;
	}
}
