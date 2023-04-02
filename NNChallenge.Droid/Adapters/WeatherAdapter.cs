using System;
using Android.Views;
using AndroidX.RecyclerView.Widget;
using NNChallenge.Droid.ViewHolders;
using NNChallenge.Interfaces;

namespace NNChallenge.Droid.Adapters
{
	public class WeatherAdapter : RecyclerView.Adapter
    {
        private IHourWeatherForecastVO[] _hourForecast;
        public WeatherAdapter(IHourWeatherForecastVO[] hourForecast)
        {
            _hourForecast = hourForecast;
        }

        public override int ItemCount => _hourForecast.Length;

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var item = _hourForecast[position];
            var weather = string.Format("{0}ºC", item.TeperatureCelcius);
            var weatherF = string.Format("{0}ºF", item.TeperatureFahrenheit);

            WeatherViewHolders viewHolder = holder as WeatherViewHolders;
            viewHolder.Bind(weather, weatherF, item.ForecastPitureURL, item.Date);
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View hoursVw = LayoutInflater.From(parent.Context).Inflate(Resource.Layout.weather_view_item, parent, false);
            return new WeatherViewHolders(hoursVw);
        }
    }
}
