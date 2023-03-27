namespace SODP.Application.ValueObjects;

public sealed record InvestorValueObject(
	int Id, 
	string Name, 
	bool ActiveStatus);
