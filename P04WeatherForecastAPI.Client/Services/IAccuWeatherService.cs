using P04WeatherForecastAPI.Client.Models;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Services
{
    public interface IAccuWeatherService
    {
        Task<Weather> GetCurrentConditions(string cityKey);
        Task<HourlyForecast[]> GetForecastFor12Hours(string cityKey);
        Task<DailyForecast> GetForecastFor1Day(string cityKey);
        Task<HourlyForecast> GetForecastFor1Hour(string cityKey);
        Task<DailyForecast[]> GetForecastFor5Days(string cityKey);
        Task<City[]> GetLocations(string locationName);
    }
}