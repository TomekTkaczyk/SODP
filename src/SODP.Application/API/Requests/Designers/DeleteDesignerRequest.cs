﻿using MediatR;

namespace SODP.Application.API.Requests.Designers;

public sealed record DeleteDesignerRequest(
	int Id) : IRequest;