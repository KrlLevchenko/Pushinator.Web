using Dodo.Primitives;

namespace Pushinator.Web.Model
{
    public class User
    {
        public Uuid Id { get; set; }

        public string Name { get; set; }

        public string? Email { get; set; }

        public string? PasswordHash { get; set; }
    }
}