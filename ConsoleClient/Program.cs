using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Newtonsoft.Json.Linq;

namespace ConsoleClient
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                CallApiClientCredentials().GetAwaiter().GetResult();
                //GetTokenResourceOwner().GetAwaiter().GetResult();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
            
            Console.ReadLine();
        }

        public static async Task CallApiClientCredentials()
        {
            var discoveryDocument = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (discoveryDocument.IsError) throw new Exception(discoveryDocument.Error);

            var tokenClient = new TokenClient(discoveryDocument.TokenEndpoint, "client-credential-client", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("test-api");
            if (tokenResponse.IsError) throw new Exception(tokenResponse.Error);

            await CallApi(tokenResponse.AccessToken);
        }

        public static async Task GetTokenResourceOwner()
        {
            var discoveryDocument = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (discoveryDocument.IsError) throw new Exception(discoveryDocument.Error);

            var tokenClient = new TokenClient(discoveryDocument.TokenEndpoint, "resource-owner-client", "password");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("KiernanL", "password", "test-api");
            if (tokenResponse.IsError) throw new Exception(tokenResponse.Error);

            await CallApi(tokenResponse.AccessToken);
        }

        private static async Task CallApi(string accessToken)
        {
            var client = new HttpClient();
            client.SetBearerToken(accessToken);

            var response = await client.GetAsync("http://localhost:5001/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}
