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
                Console.WriteLine(disco.Error);
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
                Console.WriteLine(tokenResponse.Error);
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
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content =  response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                Console.WriteLine("JArray-----             "+JArray.Parse(content));
            }
        }
    }
}
