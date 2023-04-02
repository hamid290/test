using System;
using NNChallenge.Interfaces;

namespace NNChallenge.Models.ViewModel
{
	public class HourWeatherForecastVOModel : IHourWeatherForecastVO
    {
        public DateTime date;
        public DateTime Date => date;

        public float temperatureC;
        public float TeperatureCelcius => temperatureC;

        public float temperatureF;
        public float TeperatureFahrenheit => temperatureF;

        public string pictureUrl;
        public string ForecastPitureURL => pictureUrl;
    }
}

