using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using P04WeatherForecastAPI.Client.Configuration;
using P06Shop.Shared;
using P06Shop.Shared.Cars;
using P06Shop.Shared.Services.CarService;
using P06Shop.Shared.Shop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Services.CarService
{
    internal class CarService : ICarService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;
        public CarService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings.Value;
        }

        public Task<ServiceResponse> CreateCarAsync(Car car)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse> DeleteCarAsync(int carId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<Car>>> GetCarsAsync()
        {
            var response = await _httpClient.GetAsync(_appSettings.CarsEndpoint.GetAllCarsEndpoint);
            var json = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<List<Car>>(json);
            var result = new ServiceResponse<List<Car>>
            {
                Data = content,
                Message = "Cars retrieved successfully.",
                Success = true
            };
            return result;
        }

        public Task<ServiceResponse> UpdateCarAsync(Car car)
        {
            throw new NotImplementedException();
        }
    }
}
