using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using P04WeatherForecastAPI.Client.Configuration;
using P06Shop.Shared;
using P06Shop.Shared.Cars;
using P06Shop.Shared.Repositories;
using P06Shop.Shared.Services.CarService;
using System.Net.Http;

namespace MVCClient.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            this._carRepository = carRepository;
        }

        public Task<ServiceResponse> CreateCarAsync(Car car)
        {
            return _carRepository.CreateCarAsync(car);
        }

        public Task<ServiceResponse> DeleteCarAsync(int carId)
        {
            return _carRepository.DeleteCarAsync(carId);
        }

        public async Task<ServiceResponse<List<Car>>> GetCarsAsync()
        {
            return await _carRepository.GetCarsAsync();
        }

        public Task<ServiceResponse> UpdateCarAsync(Car car)
        {
            return _carRepository.UpdateCarAsync(car);
        }
    }
}
