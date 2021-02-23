using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LinqToDB;
using MediatR;
using Pushinator.Web.Model;

namespace Pushinator.Web.Api.Users.GetAll
{
    public class Handler: IRequestHandler<Request,Response>
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public Handler(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<Response> Handle(Request request, CancellationToken ct)
        {
            var users = await _context.Users
                .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
                .ToArrayAsync(ct);
            return new Response {Users = users};
        }
    }
}