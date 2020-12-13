using System;
using System.Threading.Tasks;

namespace Pushinator.Web.Core.ExceptionHandling
{
	public interface IExceptionHandler
	{
		Task<ExceptionHandleResult> Handle(Exception exception);
	}
}