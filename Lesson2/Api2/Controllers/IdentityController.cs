using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Api2.Controllers
{
    [Authorize]
    [Route("identity")]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var claims = HttpContext.User.Claims;
            var username = claims.FirstOrDefault(x => x.Type == "username").Value;
            var role = claims.FirstOrDefault(x => x.Type == "role").Value;

            return Content($"Hello from API 2 - You Gave me this username: {username} and Role: {role}");
        }
    }
}