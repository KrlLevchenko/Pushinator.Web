using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pushinator.Web.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthController: Controller
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public Task<Handlers.Auth.Response> Post(Handlers.Auth.Request request, CancellationToken ct) => _mediator.Send(request, ct);
    }
}