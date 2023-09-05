using ServiceContracts;

namespace Services;

public class NewCitiesService : ICitiesService
{
    private List<string> _cities;

    public NewCitiesService()
    {
        _cities = new List<string>
        {
            "New City 1",
            "New City 2",
            "New City 3"
        };
    }

    public List<string> GetCities()
    {
        return _cities;
    }
}