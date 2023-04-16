using MediatR;

namespace SODP.Application.API.Requests.Licenses;

public sealed record DeleteLicenseRequest(int Id) : IRequest; 
