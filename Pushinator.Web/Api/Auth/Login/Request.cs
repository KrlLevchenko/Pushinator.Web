using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Pushinator.Web.Api.Auth.Login
{
    public class Request : IRequest<Response>
    {
        [FromBody] public Credentials Credentials { get; set; }
    }
}