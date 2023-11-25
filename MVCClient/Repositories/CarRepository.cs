using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using P04WeatherForecastAPI.Client.Configuration;
using P06Shop.Shared;
using P06Shop.Shared.Cars;
using P06Shop.Shared.Repositories;

namespace MVCClient.Repositories
{
    public class CarRepository: ICarRepository
    {
         private readonly AppSettings appSettings;
        private readonly HttpClient httpClient;

        public CarRepository(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
            this.httpClient = httpClient;
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
            var response = await httpClient.GetAsync(appSettings.CarsEndpoint.GetAll);
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