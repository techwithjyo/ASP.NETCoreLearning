using WeatherApp.Models;

namespace WeatherApp;

public interface IWeatherService
{
    List<CityWeather> GetWeatherDetails();
    CityWeather? GetWeatherByCityCode(string CityCode);
}