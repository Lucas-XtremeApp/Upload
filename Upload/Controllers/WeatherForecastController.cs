﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Upload.Controllers
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


        public class FileUpLoad
        {
            public IFormFile file { get; set; }
        }
        [HttpPost("Upload")]
        public void Upload([FromForm] FileUpLoad objfile)
        {
            //bbbbbbbbbbbb
            var uploads = Path.Combine(Directory.GetCurrentDirectory(),"uploads");
            if (!Directory.Exists(uploads)) Directory.CreateDirectory(uploads);
            if (objfile.file.Length > 0)
            {
                var filePath = Path.Combine(uploads, objfile.file.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                  objfile.file.CopyTo(fileStream);
                }
            }
        }
    }
}
