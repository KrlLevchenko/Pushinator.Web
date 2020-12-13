using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pushinator.Web.Handlers.Auth;

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

        public Task<Response> Post(Request request, CancellationToken ct) => _mediator.Send(request, ct);
    }
}