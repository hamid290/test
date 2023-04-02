using System;
using NNChallenge.Config;
using NNChallenge.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NNChallenge.Presenters
{
	public class ForecastPresenter : IWeatherForcastVO, IHourWeatherForecastVO
    {
        private readonly IForecastService _forecastService;
        private readonly IUpdateView _updateView;
        private string _city;
        private List<ForecastPresenter> _myArray;
        private DateTime _datetime;
        private float _temperatureC;
        private float _temperatureF;
        private string _imageUrl;

        public ForecastPresenter() { }

        public ForecastPresenter(IUpdateView updateView)
        {
            _updateView = updateView;
            _forecastService = ModuleLocator.Resolve<IForecastService>();
        }

        public string City => _city;

        public IHourWeatherForecastVO[] HourForecast => _myArray.ToArray();

        public DateTime Date => _datetime;

        public float TeperatureCelcius => _temperatureC;

        public float TeperatureFahrenheit => _temperatureF;

        public string ForecastPitureURL => _imageUrl;



        public async Task GetForecastByCity(string city)
        {
            try
            {
                var serviceResult = await _forecastService.GetForecastData(city);
                if (serviceResult != null)
                {
                    _myArray = new List<ForecastPresenter>();

                    _city = serviceResult.Location.Name;

                    for (int i = 0; i < serviceResult.Forecast.Forecastday.Count; i++)
                    {
                        var currentDay = serviceResult.Forecast.Forecastday[i];

                        for (int j = 0; j < currentDay.Hours.Count; j++)
                        {
                            var currentHour = currentDay.Hours[j];

                            _myArray.Add(new ForecastPresenter()
                            {
                                _datetime = DateTime.Parse(currentHour.Time),
                                _temperatureC = (float)currentHour.Temperature_c,
                                _temperatureF = (float)currentHour.Temperature_f,
                                _imageUrl = currentHour.Condition.Icon
                            });
                        }
                    }
                }
                _updateView.UpdateView();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

