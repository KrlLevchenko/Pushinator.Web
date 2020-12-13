using Dodo.Primitives;

namespace Pushinator.Web.Handlers.Users.GetById
{
    public class Response
    {
        public Uuid Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
    }
}