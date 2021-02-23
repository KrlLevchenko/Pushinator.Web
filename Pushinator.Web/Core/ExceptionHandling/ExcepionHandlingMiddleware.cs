using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Pushinator.Web.Core.ExceptionHandling
{
    public class ExcepionHandlingMiddleware
    {
        private readonly IExceptionHandlerFactory _factory;
        private readonly RequestDelegate _next;

        public ExcepionHandlingMiddleware(RequestDelegate next,
            IExceptionHandlerFactory factory)
        {
            _next = next;
            _factory = factory;
        }

        public async Task Invoke(HttpContext? context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var result = await GetResult(ex);

                if (context != null)
                {
                    context.Response.ContentType = "application/json";
                    context.Response.StatusCode = result.HttpCode;

                    await context.Response.WriteAsync(result.Body);
                }
            }
        }

        private async Task<ExceptionHandleResult> GetResult(Exception exception)
        {
            var handler = _factory.GetForOrDefault(exception.GetType());
            if (handler == null) return GetDefaultResult();

            var result = await handler.Handle(exception);

            return result ?? GetDefaultResult();
        }

        private static ExceptionHandleResult GetDefaultResult()
        {
            return new ExceptionHandleResult(500, "somethingWentWrong");
        }
    }
}