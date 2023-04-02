using System;
using NNChallenge.Interfaces;
using System.Threading.Tasks;
using NNChallenge.Models;

namespace NNChallenge.Services
{
	public class ForecastService : IForecastService
    {
        private readonly ApiClient _apiClient;
        public ForecastService()
        {
            _apiClient = new ApiClient();
        }

        public async Task<ResponseForecastModel> GetForecastData(string queryString)
        {
            return await _apiClient.GetAsync<ResponseForecastModel>(queryString);
        }
    }
}

