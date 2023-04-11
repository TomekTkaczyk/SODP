using MediatR;
using SODP.Domain.Entities;

namespace SODP.Application.Queries.Designers;

public record GetDesignerByIdQuery(int Id) : IRequest<Designer>;
