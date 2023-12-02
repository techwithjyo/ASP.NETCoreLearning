using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ConfigurationExample.Models;
using Microsoft.Extensions.Options;

namespace ConfigurationExample.Controllers;

public class HomeController : Controller
{
    //private field
    private readonly ILogger<HomeController> _logger;
    // private readonly IConfiguration _configuration;
    private readonly WeatherApiOptions _configuration;
    
    
    // public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    public HomeController(ILogger<HomeController> logger, IOptions<WeatherApiOptions> weatherApiOptions)
    {
        _logger = logger;
        // _configuration = configuration;
        _configuration = weatherApiOptions.Value;

    }
    [Route("/")]
    public IActionResult Index()
    {
        // ViewBag.MyKey = _configuration["MyKey"];
        // ViewBag.MyKey1 = _configuration.GetValue("x", 000);
        
        //Using Hierarchical Configuration
        // ViewBag.ClientId = _configuration["weatherapi:ClientId"];
        // ViewBag.ClientSecret = _configuration.GetValue("weatherapi:ClientSecret", "DefaultSecret");
        
        // ViewBag.ClientId = _configuration.GetSection("weatherapi")["ClientId"];
        // ViewBag.ClientSecret = _configuration.GetValue("weatherapi:ClientSecret", "DefaultSecret");
        
        // WeatherApiOptions options = _configuration.GetSection("weatherapi").Get<WeatherApiOptions>();

        // ViewBag.ClientId = options.ClientId;
        // ViewBag.ClientSecret = options.ClientSecret;
        
        ViewBag.ClientId = _configuration.ClientId;
        ViewBag.ClientSecret = _configuration.ClientSecret;
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}