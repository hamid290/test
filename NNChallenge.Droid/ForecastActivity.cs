
using Android.App;
using Android.OS;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using NNChallenge.Constants;
using NNChallenge.Droid.Adapters;
using NNChallenge.Interfaces;
using NNChallenge.Presenters;

namespace NNChallenge.Droid
{
    [Activity(Label = "ForecastActivity")]
    public class ForecastActivity : Activity, IUpdateView
    {
        private ForecastPresenter _presenter;
        private WeatherAdapter _adapter;
        private ProgressBar _progressBar;
        //Resources
        LinearLayout _ll_parent;
        RecyclerView _rv_weather_list;
        TextView _tv_city;
        private TextView _lblNoResult;

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_forecast);

            //get properties
            LoadResources();
            //set progress bar
            SetProgress();

            var citySelected = Intent.GetIntExtra("citySelected", 0);
            var city = LocationConstants.LOCATIONS[citySelected];

            //call service
            _progressBar.Visibility = Android.Views.ViewStates.Visible;
            _presenter = new ForecastPresenter(this);
            await _presenter.GetForecastByCity(city);
            // Create your application here
        }

        private void SetProgress()
        {
            _progressBar = new ProgressBar(this, null, Android.Resource.Attribute.ProgressBarStyleLarge);
            RelativeLayout.LayoutParams mParams = new RelativeLayout.LayoutParams(100, 100);
            mParams.AddRule(LayoutRules.CenterInParent);
            this.AddContentView(_progressBar, mParams);
        }

        private void LoadResources()
        {
            _ll_parent = FindViewById<LinearLayout>(Resource.Id.ll_parent);
            _rv_weather_list = FindViewById<RecyclerView>(Resource.Id.rv_weather_list);
            _tv_city = FindViewById<TextView>(Resource.Id.lbl_city);
            _lblNoResult = FindViewById<TextView>(Resource.Id.lb_no_result);
        }

        public void UpdateView()
        {
            //Show no result message 
            _lblNoResult.Visibility = Android.Views.ViewStates.Gone;
            if (_presenter.HourForecast.Length == 0)
            {
                _lblNoResult.Visibility = Android.Views.ViewStates.Visible;
            }

            //Set title
            Title = _presenter.City;
            _tv_city.Text = _presenter.City;

            //set layout manager
            _rv_weather_list.SetLayoutManager(new LinearLayoutManager(this));

            //create adapter
            _adapter = new WeatherAdapter(_presenter.HourForecast);
            _rv_weather_list.SetAdapter(_adapter);
            _rv_weather_list.HasFixedSize = true;

            _progressBar.Visibility = Android.Views.ViewStates.Gone;
        }
    }
}
