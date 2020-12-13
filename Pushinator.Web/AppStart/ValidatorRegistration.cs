using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Pushinator.Web.Core;

namespace Pushinator.Web.AppStart
{
	public static class ValidatorRegistration
	{
		public static IServiceCollection AddValidators(this IServiceCollection services,
			params Type[] handlerAssemblyMarkerTypes)
		{
			var assemblies = handlerAssemblyMarkerTypes.Select(t => t.GetTypeInfo().Assembly).ToArray();
			if (!assemblies.Any())
			{
				throw new ArgumentException(
					"No assemblies found to scan. Supply at least one assembly to scan for handlers.");
			}
			RegisterValidatorsAndBehavior(services, assemblies);
			return services;
		}
		private static void RegisterValidatorsAndBehavior(
			IServiceCollection services,
			IEnumerable<Assembly> assembliesToScan)
		{
			var types = assembliesToScan.SelectMany(a => a.DefinedTypes).Where(t => t.IsConcrete()).ToArray();
			var handlerTypes = types.Where(t => t.GetInterface(typeof(IRequestHandler<,>)) != null);
			var validatorTypes = types
				.Where(t =>
				{
					var @interface = t.GetInterface(typeof(IValidator<>));
					var firstGenericArgument = @interface?.GetGenericArguments()[0];
					return firstGenericArgument != null
					       && t.IsConcrete();
				});
			var validatableTypes = validatorTypes
				.ToDictionary(v => v.GetInterface(typeof(IValidator<>)).GenericTypeArguments[0], v => v);
			var requestResponseValidators = handlerTypes.Select(h =>
					h.GetInterface(typeof(IRequestHandler<,>)).GetGenericArguments()[0])
				.Where(r => validatableTypes.ContainsKey(r))
				.Select(request => (
					request,
					request.GetInterface(typeof(IRequest<>)).GetGenericArguments()[0], // response
					validatableTypes[request] // validator
				));
			foreach (var (requestType, responseType, validator) in requestResponseValidators)
			{
				var handlerArgs = new[] {requestType, responseType};
				var closedBehaviorInterfaceType = typeof(IPipelineBehavior<,>).MakeGenericType(handlerArgs);
				var closedBehaviorImplementationType =
					typeof(ValidationBehavior<,>).MakeGenericType(requestType, responseType);
				services.AddTransient(closedBehaviorInterfaceType, closedBehaviorImplementationType);
				var validatorArgs = new[] {requestType};
				var closedValidatorInterfaceType = typeof(IValidator<>).MakeGenericType(validatorArgs);
				services.AddTransient(closedValidatorInterfaceType, validator);
			}
		}
		private static Type GetInterface(this Type pluggedType, Type interfaceType)
		{
			return pluggedType
				.GetInterfaces()
				.FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType);
		}
		private static bool IsConcrete(this Type type)
		{
			return !type.GetTypeInfo().IsAbstract && !type.GetTypeInfo().IsInterface;
		}
	}}