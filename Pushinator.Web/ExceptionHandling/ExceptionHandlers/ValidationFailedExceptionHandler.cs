using System.Threading.Tasks;
using Pushinator.Web.Core.ExceptionHandling;
using Pushinator.Web.ExceptionHandling.Exceptions;

namespace Pushinator.Web.ExceptionHandling.ExceptionHandlers
{
    public class ValidationFailedExceptionHandler: ExceptionHandlerBase<ValidationFailedException>
    {
        protected override Task<ExceptionHandleResult> HandleInternal(ValidationFailedException exception) => Task.FromResult(new ExceptionHandleResult(400, exception.Message));
    }
}