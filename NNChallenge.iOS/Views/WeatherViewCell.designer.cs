// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;

namespace NNChallenge.iOS.Views
{
	[Register ("WeatherViewCell")]
	partial class WeatherViewCell
    {
        [Outlet]
        UIKit.UIImageView _imgIcon { get; set; }

        [Outlet]
        UIKit.UILabel _lblDate { get; set; }

        [Outlet]
        UIKit.UILabel _lblTime { get; set; }

        [Outlet]
        UIKit.UILabel _lblWeather { get; set; }

        [Outlet]
        UIKit.UILabel _lblWeatherF { get; set; }

        [Outlet]
        UIKit.UILabel _lblYear { get; set; }

        void ReleaseDesignerOutlets()
        {
            if (_imgIcon != null)
            {
                _imgIcon.Dispose();
                _imgIcon = null;
            }

            if (_lblTime != null)
            {
                _lblTime.Dispose();
                _lblTime = null;
            }

            if (_lblWeather != null)
            {
                _lblWeather.Dispose();
                _lblWeather = null;
            }

            if (_lblWeatherF != null)
            {
                _lblWeatherF.Dispose();
                _lblWeatherF = null;
            }

            if (_lblDate != null)
            {
                _lblDate.Dispose();
                _lblDate = null;
            }

            if (_lblYear != null)
            {
                _lblYear.Dispose();
                _lblYear = null;
            }
        }
    }
}
