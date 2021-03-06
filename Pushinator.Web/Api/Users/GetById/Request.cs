using Dodo.Primitives;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Pushinator.Web.Api.Users.GetById
{
    public class Request: IRequest<Response>
    {
        [FromRoute]public Uuid Id { get; set; }
    }
}