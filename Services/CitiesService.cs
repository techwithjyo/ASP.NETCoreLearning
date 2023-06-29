using ServiceContracts;

namespace Services
{
    public class CitiesService : ICitiesService
    {
        private List<string> _cities;

        public CitiesService()
        {
            _cities = new List<string>()
            {
                "London",
                "Paris",
                "Kolkata"
            };
        }

        public List<string> GetCities()
        {
            return _cities;
        }
    }
}