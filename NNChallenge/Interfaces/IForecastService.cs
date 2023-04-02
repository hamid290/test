using System;
using System.Threading.Tasks;
using NNChallenge.Models;

namespace NNChallenge.Interfaces
{
	public interface IForecastService
    {
        /// <summary>
        /// return an object with three days forecast information
        /// </summary>
        /// <param name="queryString">city to consult</param>
        /// <returns>ResponseForecastModel with all forecast data from this city in params in the next 3 days</returns>
        public Task<ResponseForecastModel> GetForecastData(string queryString);
    }
}

