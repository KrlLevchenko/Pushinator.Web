using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Prometheus;
using Pushinator.Web.Core.Auth;
using Pushinator.Web.Model;

namespace Pushinator.Web.Api.Auth.Login
{
    public class Handler: IRequestHandler<Request, Response>
    {
        private readonly Context _context;

        private readonly Counter _counter = Metrics.CreateCounter(
            "auth_counter",
            "Count of auth actions",
            "action_type",
            "result");
        
        public Handler(Context context)
        {
            _context = context;
        }
        
        public async Task<Response> Handle(Request request, CancellationToken ct)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Credentials.Email, ct);
            if (user != null)
            {
                var passwordHasher = new PasswordHasher<User>();
                var result = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Credentials.Password);
                if (result != PasswordVerificationResult.Failed)
                {
                    var claimIdentity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    claimIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Email));
                    claimIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

                    _counter.Labels("login", "false").Inc();
                    return Response.Success(JwtTokenGenerator.GenerateToken(claimIdentity));
                }
            }

            _counter.Labels("login", "true").Inc();
            return Response.Fail();
        }
    }
}