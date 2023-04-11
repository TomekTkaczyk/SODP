using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SODP.Application.Commands.Common;
using SODP.Domain.Entities;
using SODP.Shared.Response;
using System;
using System.Linq;
using System.Reflection;

namespace SODP.Application;

public static class DependecyInjection
{
	public static IServiceCollection AddActiveStatusCommandHandlers(
		this IServiceCollection services, 
		Assembly assembly)
	{
		var types = assembly
			.GetTypes()
			.Where(type => !type.IsAbstract && type.GetInterfaces().Contains(typeof(IActiveStatus)));

		foreach (var type in types)
		{
			var requestType = typeof(SetActiveStatusCommand<>).MakeGenericType(type);
			var handlerType = typeof(SetActiveStatusCommandHandler<>).MakeGenericType(type);
			services.AddTransient(
				typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(Unit)), 
				handlerType);
		}

		return services;
	}
}
