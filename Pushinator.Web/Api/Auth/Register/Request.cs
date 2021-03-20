using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Pushinator.Web.Api.Auth.Register
{
    public class Request: IRequest<Response>
    {
        [FromBody] public UserDto UserDto { get; set; }

    }
}