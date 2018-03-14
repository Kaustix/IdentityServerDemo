using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [Route("identity")]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var currentToken = GetCurrentToken();
            var newAccessToken = GetNewToken(currentToken).Result;

            var client = new HttpClient();
            client.SetBearerToken(newAccessToken);

            var response = client.GetAsync("http://localhost:5003/identity").Result;
            var api2Content = response.Content.ReadAsStringAsync().Result;

            return Content($"Hello from API 1 and {api2Content}");
        }

        private async Task<string> GetNewToken(string token)
        {
            var discoveryDocument = await DiscoveryClient.GetAsync("http://localhost:5000");
            var tokenClient = new TokenClient(discoveryDocument.TokenEndpoint, "test-api", "secret");
            var tokenRepsonse = await tokenClient.RequestCustomGrantAsync("delegation", "test-api-2", new { token });
            return tokenRepsonse.AccessToken;
        }

        private string GetCurrentToken()
        {
            var bearerToken = Request.Headers.FirstOrDefault(x => x.Key == "Authorization").Value;
            return bearerToken.ToString().Replace("Bearer ", string.Empty);
        }
    }
}
