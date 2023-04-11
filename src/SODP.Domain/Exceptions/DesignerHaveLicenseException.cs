namespace SODP.Domain.Exceptions;

public class DesignerHaveLicenseException : DomainException
{
	public DesignerHaveLicenseException() : base($"Designer already have this license.") { }
}
