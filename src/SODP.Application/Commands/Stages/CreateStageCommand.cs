﻿using SODP.Application.Abstractions;
using SODP.Domain.Entities;

namespace SODP.Application.Commands.Stages;

public sealed record CreateStageCommand(
	string Sign,
	string Name) : ICommand<Stage>;