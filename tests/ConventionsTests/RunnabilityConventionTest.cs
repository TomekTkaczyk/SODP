using MediatR;
using NuGet.Protocol.Plugins;
using System;
using System.Linq;
using Xunit;

namespace tests.ConventionsTests;

public class RunnabilityConventionTests
{
	[Fact]
	internal void each_cqrs_request_has_one_handler()
	{
		var requests = ConventionsHelper.classes()
			.Where(x => !x.IsAbstract)
			.Where(x => x.IsAssignableTo(typeof(IRequest)))
			.ToList();

		var handlers = ConventionsHelper.classes()
			.Where(x => !x.IsAbstract)
			.Where(x => x.IsAssignableTo(typeof(IRequestHandler)))
			.ToList();

		Assert.NotEmpty(requests);

		foreach(var request in requests)
		{
			var handler_type = typeof(IRequestHandler<>)
				.MakeGenericType(request);

			var request_handlers = handlers
				.Where(x => x.GetInterfaces().Contains(handler_type));

			Assert.Single(request_handlers,request);
		}
	}

	[Fact]
	internal void each_cqrs_request_that_implements_IRequest_interface_is_of_type_record()
	{
		var types = ConventionsHelper.classes()
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
		var types = ConventionsHelper.classes()
			.Where(x => !x.IsAbstract)
			.Where(x => typeof(IRequest).IsAssignableFrom(x));

		Assert.NotEmpty(types);

		var cqrs_requests_that_are_not_sealed = types
			.Where(x => !x.IsSealed)
			.ToList();

		Assert.Empty(cqrs_requests_that_are_not_sealed);
	}
}
