using Dodo.Primitives;

namespace Pushinator.Web.Handlers.Users.GetAll
{
    public class UserDto
    {
        public Uuid Id { get; set; }
        public string Name { get; set; }
        public string? Email { get; set; }
    }
}