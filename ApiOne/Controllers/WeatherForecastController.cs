using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiOne.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
       
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHttpClientFactory _http;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory http)
        {
            _logger = logger;
            _http = http;
           
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            //HttpClient client=_http.CreateClient();
            // var con = _http;
            // var df = client.Request.Headers.ToString();\
            var req = Request;
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
               
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })  
            .ToArray();
        }
        [HttpPost]
        public string Post()
        {
            var req = Request;
            return "Nice";
        }
    }
}
