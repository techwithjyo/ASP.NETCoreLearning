using ServiceContracts;

namespace Services
{
    public class CitiesService : ICitiesService, IDisposable
    {
        private List<string> _cities;
        private Guid _serviceInstanceId;
        public Guid ServiceInstanceId
        {
            get
            {
                return _serviceInstanceId;
            }
        }

        public CitiesService()
        {
            _serviceInstanceId = Guid.NewGuid();
            _cities = new List<string>()
            {
                "London",
                "Paris",
                "Kolkata",
                "Chicago",
                "Mayami",
                "California"
            };
        }
        

        public List<string> GetCities()
        {
            return _cities;
        }

        public void Dispose()
        {
            //TO Do: add login to close DB Connection
        }
    }
}