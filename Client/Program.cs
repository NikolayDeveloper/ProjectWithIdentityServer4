using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            // discover endpoints from metadata
            var client = new HttpClient();
            var disco =  client.GetDiscoveryDocumentAsync("http://localhost:5000").GetAwaiter().GetResult();
            if (disco.IsError)
            {
                Console.WriteLine("----------------if something wrong----------------------");
                Console.WriteLine(disco.Error);
                Console.WriteLine("----------------if something wrong----------------------");
                return;
            }
            else
            {
                Console.WriteLine("Raw----            "+disco.Raw);
                Console.WriteLine("_______________________________________________________");
            }

            // request token
            var tokenResponse =  client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,
                
                ClientId = "client",
                ClientSecret = "secret",
                Scope = "ApiOne"
            }).GetAwaiter().GetResult();

            if (tokenResponse.IsError)
            {
                Console.WriteLine("----------------if something wrong----------------------");
                Console.WriteLine(tokenResponse.Error);
                Console.WriteLine("----------------if something wrong----------------------");
                return;
            }

            Console.WriteLine("tokenResponse-------         "+tokenResponse.Json);
            Console.WriteLine("_______________________________________________________");

            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            // Here we go to the ApiOne
            var response =  apiClient.GetAsync("http://localhost:5001/identity").GetAwaiter().GetResult();
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("----------------if something wrong----------------------");
                Console.WriteLine(response.StatusCode);
                Console.WriteLine("----------------if something wrong----------------------");
            }
            else
            {
                var content =  response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                Console.WriteLine("JArray-----             "+JArray.Parse(content));
            }
        }
    }
}
