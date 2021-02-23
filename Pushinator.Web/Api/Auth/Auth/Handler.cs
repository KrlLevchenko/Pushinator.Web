using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Pushinator.Web.Core.Auth;
using Pushinator.Web.Model;

namespace Pushinator.Web.Api.Auth.Auth
{
    public class Handler: IRequestHandler<Request, Response>
    {
        private readonly Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public Handler(Context context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
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

                    // await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    //     new ClaimsPrincipal(claimIdentity));
                    
                    return Response.Success(JwtTokenGenerator.GenerateToken(claimIdentity));
                }
            }

            return Response.Fail();
        }
    }
}