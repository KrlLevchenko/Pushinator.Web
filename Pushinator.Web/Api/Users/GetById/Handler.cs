using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using LinqToDB;
using MediatR;
using Pushinator.Web.ExceptionHandling.Exceptions;
using Pushinator.Web.Model;

namespace Pushinator.Web.Api.Users.GetById
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
            var user = await _context.Users
                .Where(x => x.Id == request.Id)
                .ProjectTo<Response>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(ct);
            if (user == null)
            {
                throw new EntityNotFoundException(nameof(User), $"id = {request.Id}");
            }
            
            return user;
        }
    }
}