using MediatR;

namespace SODP.Application.API.Requests.Designers;

public sealed record DeleteLicenseRequest(int LicenseId) : IRequest;
