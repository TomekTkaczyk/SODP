using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SODP.Domain.ValueObjects;

public abstract record ValueObject
{
	public abstract IEnumerable<object> GetAtomicValues();

	private bool ValuesAreEqual(ValueObject other)
	{
		return GetAtomicValues().SequenceEqual(other.GetAtomicValues());
	}
}
