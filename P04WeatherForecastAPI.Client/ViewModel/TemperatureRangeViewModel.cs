using P04WeatherForecastAPI.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.ViewModel
{
    public class TemperatureRangeViewModel
    {
        public string TemperatureRangeValue { get; set; }

        public TemperatureRangeViewModel(TemperatureRange temperatureRange)
        {
            TemperatureRangeValue = string.Format("{0}-{1} {2}", temperatureRange.Minimum.Value, temperatureRange.Maximum.Value, temperatureRange.Maximum.Unit);
        }
    }
}
