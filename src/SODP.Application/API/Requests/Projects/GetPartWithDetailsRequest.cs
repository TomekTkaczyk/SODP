﻿using MediatR;
using SODP.Shared.DTO;
using SODP.Shared.Response;
using System.Collections.Generic;

namespace SODP.Application.API.Requests.Projects;

public sealed record GetPartWithDetailsRequest(int ProjectPartId) : IRequest<ApiResponse<ProjectPartDTO>>;