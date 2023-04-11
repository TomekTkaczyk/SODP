using MediatR;
using SODP.Domain.Entities;
using System.Collections.Generic;

namespace SODP.Application.Queries.Designers;

public record GetDesignerWithDetailsQuery(int DesignerId) : IRequest<Designer>;
