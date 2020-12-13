using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Pushinator.Web.Handlers.Auth
{
    public class Request : IRequest<Response>
    {
        [FromBody] public Credentials Credentials { get; set; }
    }
}