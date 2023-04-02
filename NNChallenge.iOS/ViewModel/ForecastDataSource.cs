using System;
using Foundation;
using NNChallenge.Interfaces;
using NNChallenge.iOS.Views;
using UIKit;

namespace NNChallenge.iOS.ViewModel
{
	public class ForecastDataSource : UITableViewSource
    {
        private IHourWeatherForecastVO[] _hourForecast;
        public ForecastDataSource(IHourWeatherForecastVO[] hourForecast)
        {
            _hourForecast = hourForecast;
        }

        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("WeatherViewCell", indexPath) as WeatherViewCell;

            var imgUrl = _hourForecast[indexPath.Row].ForecastPitureURL;
            var temperatureC = _hourForecast[indexPath.Row].TeperatureCelcius;
            var temperatureF = _hourForecast[indexPath.Row].TeperatureFahrenheit;
            var weather = string.Format("{0}ºC", temperatureC);
            var weatherF = string.Format("{0}ºF", temperatureF);
            var datetime = _hourForecast[indexPath.Row].Date;
            cell.Bind(weather, weatherF, imgUrl, datetime);



            return cell;
        }

        public override nint RowsInSection(UITableView tableView, nint section)
        {
            return _hourForecast.Length;
        }

        public override void WillDisplay(UITableView tableView, UITableViewCell cell, NSIndexPath indexPath)
        {
            if (cell.RespondsToSelector(new ObjCRuntime.Selector("setSeparatorInset:"))) cell.SeparatorInset = UIEdgeInsets.Zero;
            if (cell.RespondsToSelector(new ObjCRuntime.Selector("setLayoutMargins:"))) cell.LayoutMargins = UIEdgeInsets.Zero;
        }
    }
}
