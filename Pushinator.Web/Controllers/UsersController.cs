using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Pushinator.Web.Controllers
{
    [Route("api/[controller]")]
    public class UsersController: Controller
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<Handlers.Users.GetAll.Response> Get(Handlers.Users.GetAll.Request request, CancellationToken ct) => _mediator.Send(request, ct);
        
        [HttpGet("{id}")]
        public Task<Handlers.Users.GetById.Response> GetById(Handlers.Users.GetById.Request request, CancellationToken ct) => _mediator.Send(request, ct);
    }
}