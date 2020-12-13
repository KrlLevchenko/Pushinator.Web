using System.Threading;
using System.Threading.Tasks;
using LinqToDB;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Pushinator.Web.Model;

namespace Pushinator.Web.Handlers.Auth
{
    public class Handler: IRequestHandler<Request, Response>
    {
        private readonly Context _context;

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
                    return Response.Success("token_will_be_here");
                }
            }

            return Response.Fail();
        }
    }
}