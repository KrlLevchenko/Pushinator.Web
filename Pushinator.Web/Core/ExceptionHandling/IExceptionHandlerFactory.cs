using System;

namespace Pushinator.Web.Core.ExceptionHandling
{
	public interface IExceptionHandlerFactory
	{
		IExceptionHandler? GetForOrDefault(Type type);
	}
}