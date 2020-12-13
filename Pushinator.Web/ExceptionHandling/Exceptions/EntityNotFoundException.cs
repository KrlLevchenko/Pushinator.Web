using System;

namespace Pushinator.Web.ExceptionHandling.Exceptions
{
    public class EntityNotFoundException: Exception
    {
        public EntityNotFoundException(string entityName, string searchParams): base($"{entityName} not found by using {searchParams}")
        {
        }
    }
}