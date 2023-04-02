using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace NNChallenge.Services
{
	public class ApiClient
    {
        private string BaseUrl = "http://api.weatherapi.com/v1/forecast.json?key=898147f83a734b7dbaa95705211612&q={0}&days=3&aqi=no&alerts=no";// replace query with params
        private readonly HttpClient _apiClient;

        public ApiClient()
        {
            _apiClient = new HttpClient()
            {
                BaseAddress = new Uri(BaseUrl)
            };
        }

        public async Task<T> GetAsync<T>(string queryString)
        {
            string relativeUrl = string.Format(BaseUrl, queryString);

            HttpResponseMessage response = await _apiClient.GetAsync(relativeUrl);
            var result = CastResponseAsync(response);
            return JsonConvert.DeserializeObject<T>(result);
        }

        private string CastResponseAsync(HttpResponseMessage response)
        {
            string responseString = response.Content.ReadAsStringAsync().Result;

            if (response.IsSuccessStatusCode)
            {
                return responseString;
            }
            else
            {
                return null;
            }
        }
    }
}

