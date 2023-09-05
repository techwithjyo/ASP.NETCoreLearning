using Microsoft.AspNetCore.Mvc;
using ServiceContracts;
using Services;

namespace DIExample.Controllers
{
    public class HomeController : Controller
    {
        ////Older Implementation
        //private readonly CitiesService _citiesService; 

        //New Implementation
        private readonly ICitiesService _citiesService;
        public HomeController(ICitiesService citiesService)
        {
            // _citiesService = new CitiesService();
            _citiesService = citiesService;
        }
        [Route("/")]
        public IActionResult Index()
        {
            List<string> cities = _citiesService.GetCities();
            return View(cities);
        }
    }
}
