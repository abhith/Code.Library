using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreApp.Models;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AspNetCoreApp.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("flurl")]
        public async Task Flurl()
        {
            await "https://piggyvault.in/swagger/v1/swagger.json".GetJsonAsync();
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet]
        [Route("logs")]
        public void Logs()
        {
            _logger.LogTrace("First a TRACE");
            _logger.LogDebug("Then a DEBUG message");
            _logger.LogInformation("And some INFORMATION");
            _logger.LogWarning("This is a WARNING");
            _logger.LogError("There comes an ERROR");
            _logger.LogCritical("And it reached CRITICAL stage");
        }

        [HttpPost]
        [Route("plain")]
        public IEnumerable<WeatherForecast> NormalContentPost(NormalContent input)
        {
            _logger.LogInformation("Posted {@NormalContent}", input);

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
        [Route("secret")]
        public IEnumerable<WeatherForecast> SecretContentPost(SensitiveContent input)
        {
            _logger.LogInformation("Posted {@SensitiveContent}", input);

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}