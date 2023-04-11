using MediatR;

namespace SODP.Application.API.Requests.Designers;

public sealed record ChangeDesignerNameRequest(
    int Id,
    string Title,
    string Firstname,
    string Lastname) : IRequest;
