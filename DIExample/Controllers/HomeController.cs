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
        public HomeController()
        {
            _citiesService = null;//new CitiesService();
        }
        [Route("/")]
        public IActionResult Index()
        {
            List<string> cities = _citiesService.GetCities();
            return View(cities);
        }
    }
}
