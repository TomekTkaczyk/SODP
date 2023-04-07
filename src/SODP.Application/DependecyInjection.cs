using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SODP.Application.Commands.Common;
using SODP.Domain.Entities;
using SODP.Shared.Response;
using System;
using System.Collections.Generic;
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
			.Where(type => !type.IsAbstract && type.GetInterfaces().Contains(typeof(IActiveStatus)))
			.ToList();

		foreach (var type in types)
		{
			var requestType = typeof(SetActiveStatusCommand<>).MakeGenericType(type);
			var handlerType = typeof(SetActiveStatusCommandHandler<>).MakeGenericType(type);
			services.AddTransient(
				typeof(IRequestHandler<,>).MakeGenericType(requestType, typeof(ApiResponse)), 
				handlerType);
		}


		//services.AddTransient(
		//	typeof(IRequestHandler<SetActiveStatusCommand<Branch>, ApiResponse>),
		//	typeof(SetActiveStatusCommandHandler<Branch>));
		//services.AddTransient(
		//	typeof(IRequestHandler<SetActiveStatusCommand<Designer>, ApiResponse>),
		//	typeof(SetActiveStatusCommandHandler<Designer>));
		//services.AddTransient(
		//	typeof(IRequestHandler<SetActiveStatusCommand<Investor>, ApiResponse>),
		//	typeof(SetActiveStatusCommandHandler<Investor>));
		//services.AddTransient(
		//	typeof(IRequestHandler<SetActiveStatusCommand<Part>, ApiResponse>),
		//	typeof(SetActiveStatusCommandHandler<Part>));
		//services.AddTransient(
		//	typeof(IRequestHandler<SetActiveStatusCommand<Stage>, ApiResponse>),
		//	typeof(SetActiveStatusCommandHandler<Stage>));

		return services;
	}


}
