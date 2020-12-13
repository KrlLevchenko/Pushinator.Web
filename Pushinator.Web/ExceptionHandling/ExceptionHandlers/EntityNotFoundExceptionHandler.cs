using System.Threading.Tasks;
using Pushinator.Web.Core.ExceptionHandling;
using Pushinator.Web.ExceptionHandling.Exceptions;

namespace Pushinator.Web.ExceptionHandling.ExceptionHandlers
{
    public class EntityNotFoundExceptionHandler: ExceptionHandlerBase<EntityNotFoundException>
    {
        protected override Task<ExceptionHandleResult> HandleInternal(EntityNotFoundException exception) => Task.FromResult(new ExceptionHandleResult(404, exception.Message));
    }
}