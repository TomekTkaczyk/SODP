using System;

namespace SODP.Domain.Exceptions;
	
public abstract class DomainException : Exception
{
	protected DomainException(string message) : base(message) { }
}
