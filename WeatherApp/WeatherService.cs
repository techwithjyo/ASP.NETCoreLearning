using WeatherApp.Models;

namespace WeatherApp;

public class WeatherService : IWeatherService
{
    private List<CityWeather> _weathers;

    public WeatherService()
    {
        _weathers = new List<CityWeather>()
        {
            new CityWeather() {CityUniqueCode = "LDN", CityName = "London", DateAndTime = new DateTime(2019,05,09,9,15,0),  TemperatureFarhenheit = 33},
            new CityWeather() {CityUniqueCode = "NYC", CityName = "London", DateAndTime = new DateTime(2019,05,09,9,15,0),  TemperatureFarhenheit = 60},
            new CityWeather() {CityUniqueCode = "PAR", CityName = "Paris", DateAndTime = new DateTime(2019,05,09,9,15,0),  TemperatureFarhenheit = 82}
        };
    }

    public List<CityWeather> GetWeatherDetails()
    {
        return _weathers;
    }
    
    public CityWeather? GetWeatherByCityCode(string CityCode)
    {
        return _weathers.Find(a => a.CityUniqueCode == CityCode);
    }
}