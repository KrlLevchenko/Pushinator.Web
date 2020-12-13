using System.Threading;
using System.Threading.Tasks;
using Dodo.Primitives;
using LinqToDB;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pushinator.Web.Model;

namespace Pushinator.Web.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class SeedController: Controller
    {
        private readonly Context _context;

        public SeedController(Context context)
        {
            _context = context;
        }
        
        [HttpGet("")]
        public async Task<IActionResult> Test(CancellationToken ct)
        {
            if (await _context.Users.AnyAsync(x => x.Email == "a@a.a", ct))
            {
                return Conflict();
            }
            
            var user = new User
            {
                Email = "a@a.a",
                PasswordHash = null,
                Id = Uuid.NewMySqlOptimized(),
                Name = "a"
            };
            var passwordHasher = new PasswordHasher<User>();
            user.PasswordHash = passwordHasher.HashPassword(user, "a");
            await _context.InsertAsync(user,token: ct);
            return Ok();
        }
    }
}