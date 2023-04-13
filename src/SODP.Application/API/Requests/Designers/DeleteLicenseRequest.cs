using MediatR;

namespace SODP.Application.API.Requests.Designers;

public record DeleteLicenseRequest(int LicenseId) : IRequest;
