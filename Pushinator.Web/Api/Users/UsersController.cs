using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Pushinator.Web.Api.Users
{
    [Route("api/[controller]")]
    public class UsersController: Controller
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<GetAll.Response> Get(GetAll.Request request, CancellationToken ct) => _mediator.Send(request, ct);
        
        [HttpGet("{id}")]
        public Task<GetById.Response> GetById(GetById.Request request, CancellationToken ct) => _mediator.Send(request, ct);
    }
}