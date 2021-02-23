using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pushinator.Web.Controllers
{
    [AllowAnonymous]
    public class AuthExternalController: Controller
    {
        public IActionResult Microsoft()
        {
            return new ChallengeResult(
                MicrosoftAccountDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action(nameof(GoogleResponse))
                });
        }
        
        public async Task<IActionResult> MicrosoftResponse()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync("Microsoft");

            if (!authenticateResult.Succeeded)
                return BadRequest(); // TODO: Handle this better.
            var externalLoginData = ExternalLoginData.CreateFrom(authenticateResult);

            return Ok(externalLoginData);
        }
        
        
        public IActionResult Google()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(GoogleResponse))
            };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }
        
        public async Task<IActionResult> GoogleResponse()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync("Google");

            if (!authenticateResult.Succeeded)
                return BadRequest(); // TODO: Handle this better.
            var externalLoginData = ExternalLoginData.CreateFrom(authenticateResult!);

            return Ok(externalLoginData);
        }
        
        public IActionResult Facebook()
        {
            return new ChallengeResult(
                FacebookDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action(nameof(FacebookResponse))
                });
        }
        
        public async Task<IActionResult> FacebookResponse()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync("Facebook");

            if (!authenticateResult.Succeeded)
                return BadRequest(); // TODO: Handle this better.
            var externalLoginData = ExternalLoginData.CreateFrom(authenticateResult!);
            return Ok(externalLoginData);
        }
        
        private class ExternalLoginData
        {
            private ExternalLoginData(string id, string name, string? email)
            {
                Id = id;
                Name = name;
                Email = email;
            }

            public static ExternalLoginData CreateFrom(AuthenticateResult authenticateResult)
            {
               var claimsArray = authenticateResult.Principal.Claims.ToDictionary(x=>x.Type, x=>x.Value);
               var id = claimsArray[ClaimTypes.NameIdentifier];
               var name = claimsArray[ClaimTypes.Name];
               var email = claimsArray.ContainsKey(ClaimTypes.Email) ? claimsArray[ClaimTypes.Email] : null;
               return new ExternalLoginData(id, name, email);
            }

            public string Id { get; }
            public string Name { get; }
            public string? Email { get; }
        }
        
        
    }

    
}