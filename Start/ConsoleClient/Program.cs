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
                //CallApiClientCredentials().GetAwaiter().GetResult();
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
        }

        public static async Task GetTokenResourceOwner()
        {
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
