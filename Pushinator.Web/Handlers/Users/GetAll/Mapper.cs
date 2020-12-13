using AutoMapper;
using Pushinator.Web.Model;

namespace Pushinator.Web.Handlers.Users.GetAll
{
    public class Mapper: Profile
    {
        public Mapper()
        {
            CreateMap<User, UserDto>();
        }
    }
}