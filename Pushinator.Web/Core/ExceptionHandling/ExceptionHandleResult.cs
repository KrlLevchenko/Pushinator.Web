namespace Pushinator.Web.Core.ExceptionHandling
{
	public class ExceptionHandleResult
	{
		public int HttpCode { get; }
		public string Body { get; }

		public ExceptionHandleResult(int httpCode, string body)
		{
			HttpCode = httpCode;
			Body = body;
		}
	}
}
