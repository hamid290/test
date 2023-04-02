using System;
using Newtonsoft.Json;

namespace NNChallenge.Models.Forecast
{
	public class ConditionModel
    {
        public string text { get; set; }
        [JsonProperty("icon")]
        public string Icon { get; set; }
        public int code { get; set; }
    }
}

