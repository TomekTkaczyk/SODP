using MediatR;
using SODP.Shared.DTO;

namespace SODP.Application.API.Requests.Parts;

public sealed record UpdatePartRequest(int Id, PartDTO Part) : IRequest;
