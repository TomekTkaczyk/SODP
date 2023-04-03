using SODP.Application.Abstractions;
using SODP.Domain.Entities;

namespace SODP.Application.Commands.Projects;

public sealed record CreateProjectCommand(
	string Number,
	string StageSign,
	string Name,
	string Description) : ICommand<Project>;
