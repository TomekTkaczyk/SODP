using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace tests.ConventionsTests;

public class RunnabilityConventionTests
{
	[Fact]
	internal void each_cqrs_request_has_one_handler()
	{
		var requests_that_have_different_number_of_handlers_than_one = new List<(Type,int)>();

		var requests = ConventionsHelper.Classes()
			.Where(x => !x.IsAbstract)
			.Where(x => x.IsAssignableTo(typeof(IRequest)))
			.ToList();

		var handlers = ConventionsHelper.Classes()
			.Where(type => !type.IsAbstract && type.GetInterfaces()
				.Any(i =>
					i.IsGenericType &&
					i.GetGenericTypeDefinition() == typeof(IRequestHandler<>))
					)
			.ToList();

		Assert.NotEmpty(requests);
		Assert.NotEmpty(handlers);

		foreach (var request in requests)
		{
			var handler_type = typeof(IRequestHandler<>)
				.MakeGenericType(request);

			var request_handlers = handlers
				.Where(x => x.GetInterfaces().Any(i => i == handler_type))
				.ToList();

			if (request_handlers.Count != 1)
			{
				requests_that_have_different_number_of_handlers_than_one.Add(new(request, request_handlers.Count));
			}
		}

		Assert.Empty(requests_that_have_different_number_of_handlers_than_one);
	}

	[Fact]
	internal void each_cqrs_request_that_implements_IRequest_interface_is_of_type_record()
	{
		var types = ConventionsHelper.Classes()
			.Where(x => !x.IsAbstract)
			.Where(x => typeof(IRequest).IsAssignableFrom(x));

		Assert.NotEmpty(types);

		var cqrs_requests_that_are_not_record = types
			.Where(x => !x.IsRecord())
			.ToList();

		Assert.Empty(cqrs_requests_that_are_not_record);
	}

	[Fact]
	internal void each_cqrs_request_that_implements_IRequest_interface_is_sealed()
	{
		var types = ConventionsHelper.Classes()
			.Where(x => !x.IsAbstract)
			.Where(x => typeof(IRequest).IsAssignableFrom(x));

		Assert.NotEmpty(types);

		var cqrs_requests_that_are_not_sealed = types
			.Where(x => !x.IsSealed)
			.ToList();

		Assert.Empty(cqrs_requests_that_are_not_sealed);
	}
}
