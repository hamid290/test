using System;
using System.Net.Http;
using System.Threading.Tasks;
using Foundation;
using UIKit;

namespace NNChallenge.iOS.Views
{
	public partial class WeatherViewCell : UITableViewCell
	{
		public static readonly NSString Key = new NSString ("WeatherViewCell");
		public static readonly UINib Nib;

		static WeatherViewCell ()
		{
			Nib = UINib.FromName ("WeatherViewCell", NSBundle.MainBundle);
		}

		protected WeatherViewCell (IntPtr handle) : base (handle)
		{
            // Note: this .ctor should not contain any initialization logic.
        }

        public void Bind(string weather, string weatherF, string imageUrl, DateTime time)
        {
            _lblWeather.Text = weather;
            _lblWeatherF.Text = weatherF;
            _lblDate.Text = time.ToString("MMM dd");
            _lblYear.Text = time.Year.ToString();
            _lblTime.Text = time.ToString("hh:mm tt");

            BeginInvokeOnMainThread(async () => {
                _imgIcon.Image = await LoadImage(imageUrl);
            });
        }

        public async Task<UIImage> LoadImage(string imageUrl)
        {
            var httpClient = new HttpClient();
            var defaultUrl = "https://cdn.weatherapi.com/weather/64x64/day/266.png";
            Task<byte[]> contentsTask;
            try
            {
                contentsTask = httpClient.GetByteArrayAsync("https:" + imageUrl);
            }
            catch (Exception ex)
            {
                contentsTask = httpClient.GetByteArrayAsync(defaultUrl);
            }

            // await! control returns to the caller and the task continues to run on another thread
            var contents = await contentsTask;

            // load from bytes
            return UIImage.LoadFromData(NSData.FromArray(contents));
        }
    }
}
