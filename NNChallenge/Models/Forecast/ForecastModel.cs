using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace NNChallenge.Models.Forecast
{
	public class ForecastModel
    {
        [JsonProperty("forecastday")]
        public List<ForecastdayModel> Forecastday { get; set; }
    }
}

