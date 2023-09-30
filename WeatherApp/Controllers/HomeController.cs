using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WeatherApp.Models;

namespace WeatherApp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IWeatherService _weatherService;

    public HomeController(ILogger<HomeController> logger, IWeatherService weatherService)
    {
        _logger = logger;
        _weatherService = weatherService;
    }

    public IActionResult Index()
    {
        List<CityWeather> weather = _weatherService.GetWeatherDetails();
        return View(weather);
    }

    public IActionResult Privacy(string cityCode)
    {
        List<CityWeather> weather = new List<CityWeather>();
        weather.Add(_weatherService.GetWeatherByCityCode(cityCode));
        return View(weather);
    }

    [Route("/weather/{cityCode}")]
    public IActionResult Weather(string cityCode)
    {
        List<CityWeather> weather = new List<CityWeather>();
        if (_weatherService.GetWeatherByCityCode(cityCode) != null)
            weather.Add(_weatherService.GetWeatherByCityCode(cityCode));

        else
        {
            weather.Add(new CityWeather()
            {
                CityName = "No Data Found",
                CityUniqueCode = "No Data",
                DateAndTime = new DateTime(2023, 01, 01),
                TemperatureFarhenheit = 10
            });
            return View(weather);
        }

        return View(weather);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}