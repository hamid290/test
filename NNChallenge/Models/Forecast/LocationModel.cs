using System;
using Newtonsoft.Json;

namespace NNChallenge.Models.Forecast
{
    public class LocationModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("region")]
        public string Region { get; set; }
        [JsonProperty("country")]
        public string Country { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string tz_id { get; set; }
        public int localtime_epoch { get; set; }
        [JsonProperty("localtime")]
        public string Localtime { get; set; }
    }
}

