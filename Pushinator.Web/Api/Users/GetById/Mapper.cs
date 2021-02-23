using AutoMapper;
using Pushinator.Web.Model;

namespace Pushinator.Web.Api.Users.GetById
{
    public class Mapper: Profile
    {
        public Mapper()
        {
            CreateMap<User, Response>();
        }
    }
}