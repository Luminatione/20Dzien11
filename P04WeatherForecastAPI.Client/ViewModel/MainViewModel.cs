using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using P04WeatherForecastAPI.Client.Models;
using P04WeatherForecastAPI.Client.Services;

namespace P04WeatherForecastAPI.Client.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        private CityViewModel selectedCity;

        [ObservableProperty]
        private TemperatureRangeViewModel currentTemperatureDailyForecast;
        [ObservableProperty]
        private TemperatureViewModel currentTemperatureHourlyForecast;
        [ObservableProperty]
        private TemperatureViewModel currentTemperatureViewModel;
        [ObservableProperty]
        private ForecastLabelViewModel dailyForecastLabel;
        [ObservableProperty]
        private ForecastLabelViewModel hourlyForecastLabel;
        [ObservableProperty]
        private bool canShowNextForecast = false;

        public IAccuWeatherService AccuWeatherService { get; set; }

        public ObservableCollection<CityViewModel> Cities { get; set; }

        private int currentHour = 0;
        private int currentDay = 0;
        private List<HourlyForecast>? hourlyForecasts;
        private List<DailyForecast>? dailyForecasts;

        public CityViewModel SelectedCity { get => selectedCity; set
            {
                selectedCity = value;
                OnPropertyChanged();
                LoadWeather();
                LoadFirstForecasts();
            } }

        public MainViewModel(IAccuWeatherService accuWeatherService)
        {
            AccuWeatherService = accuWeatherService;
            Cities = new ObservableCollection<CityViewModel>();
            DailyForecastLabel = new ForecastLabelViewModel("Day", 1);
            HourlyForecastLabel = new ForecastLabelViewModel("Hour", 1);
        }

        [RelayCommand]
        public async Task LoadCities(string locationName)
        {
            // podejście nr 2:
            var cities = await AccuWeatherService.GetLocations(locationName);
            Cities.Clear();
            foreach (var city in cities)
                Cities.Add(new CityViewModel(city));
        }

        [RelayCommand]
        public async Task ShowNextHourlyForecast(string locationName)
        {
            currentHour++;
            if (currentHour == 12)
            {
                currentHour = 0;
            }
            HourlyForecastLabel = new ForecastLabelViewModel("Hours", currentHour + 1);

            if (hourlyForecasts == null)
            {
                hourlyForecasts = new List<HourlyForecast>(await AccuWeatherService.GetForecastFor12Hours(SelectedCity.Key));
            }
            CurrentTemperatureHourlyForecast = new TemperatureViewModel(hourlyForecasts[currentHour].Temperature);
        }

        [RelayCommand]
        public async Task ShowNextDailyForecast(string locationName)
        {
            currentDay++;
            if (currentDay == 5)
            {
                currentDay = 0;
            }
            DailyForecastLabel = new ForecastLabelViewModel("Days", currentDay + 1);

            if (dailyForecasts == null)
            {
                dailyForecasts = new List<DailyForecast>(await AccuWeatherService.GetForecastFor5Days(SelectedCity.Key));
            }
            CurrentTemperatureDailyForecast = new TemperatureRangeViewModel(dailyForecasts[currentDay].Temperature);
        }

        private async void LoadWeather()
        {
            if (SelectedCity != null)
            {
                ResetForecasts();
                var temperature = (await AccuWeatherService.GetCurrentConditions(SelectedCity.Key)).Temperature;
                CurrentTemperatureViewModel = new TemperatureViewModel(new SingleUnitTemperature { UnitType = temperature.UnitType, Value = temperature.Imperial.Value});
            }
        }

        private void ResetForecasts()
        {
            hourlyForecasts = null;
            dailyForecasts = null;
            currentDay = 0;
            currentHour = 0;
        }

        private async void LoadFirstForecasts()
        {
            if (SelectedCity != null)
            {
                CanShowNextForecast = true;
                SingleUnitTemperature houryTemperature = (await AccuWeatherService.GetForecastFor1Hour(SelectedCity.Key)).Temperature;
                CurrentTemperatureHourlyForecast = new TemperatureViewModel(new SingleUnitTemperature { UnitType = houryTemperature.UnitType, Value = houryTemperature.Value });

                TemperatureRange dailyTemperature = (await AccuWeatherService.GetForecastFor1Day(SelectedCity.Key)).Temperature;
                CurrentTemperatureDailyForecast = new TemperatureRangeViewModel(new TemperatureRange { Maximum = dailyTemperature.Maximum, Minimum = dailyTemperature.Minimum });
            }
        }
    }
}