using System;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using NNChallenge.Droid.Utils;

namespace NNChallenge.Droid.ViewHolders
{
	public class WeatherViewHolders : RecyclerView.ViewHolder
    {
        private readonly TextView _lblWeather;
        private readonly TextView _lblWeatherF;
        private readonly TextView _lblTime;
        private readonly TextView _lblDate;
        private readonly TextView _lblYear;
        private readonly ImageView _imgWeather;

        public WeatherViewHolders(View itemView) : base(itemView)
        {
            _lblWeather = itemView.FindViewById<TextView>(Resource.Id.lb_weather);
            _lblWeatherF = itemView.FindViewById<TextView>(Resource.Id.lb_weather_f);
            
            _lblTime = itemView.FindViewById<TextView>(Resource.Id.lbl_time);
            _lblDate = itemView.FindViewById<TextView>(Resource.Id.lbl_date);
            _lblYear = itemView.FindViewById<TextView>(Resource.Id.lbl_year);
            _imgWeather = itemView.FindViewById<ImageView>(Resource.Id.img_weather);
        }

        public void Bind(string weather, string weatherF, string imageUrl, DateTime time)
        {
            _lblWeather.Text = weather;
            _lblWeatherF.Text = weatherF;
            _lblDate.Text = time.ToString("MMM dd");
            _lblYear.Text = time.Year.ToString();
            _lblTime.Text = time.ToString("hh:mm tt");

            //Set image from url
            var imageBitmap = LoadImageHelper.GetImageBitmapFromUrl(imageUrl);
            _imgWeather.SetImageBitmap(imageBitmap);
        }

    }
}
