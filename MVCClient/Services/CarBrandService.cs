using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using P04WeatherForecastAPI.Client.Configuration;
using P06Shop.Shared;
using P06Shop.Shared.Cars;
using P06Shop.Shared.Repositories;
using P06Shop.Shared.Services.CarService;
using System.Net.Http;
using System.Text;

namespace MVCClient.Services
{
    public class CarBrandService : ICarBrandService
    {
        private readonly ICarBrandRepository _carBrandRepository;

        public CarBrandService(ICarBrandRepository carBrandRepository)
        {
            this._carBrandRepository = carBrandRepository;
        }

        public async Task<ServiceResponse> CreateCarBrandAsync(CarBrand carBrand)
        {
            return await _carBrandRepository.CreateCarBrandAsync(carBrand);
        }

        public async Task<ServiceResponse> DeleteCarBrandAsync(int carBrandId)
        {
            return await _carBrandRepository.DeleteCarBrandAsync(carBrandId);
        }

        public async Task<ServiceResponse<List<CarBrand>>> GetCarBrandsAsync()
        {
            return await _carBrandRepository.GetCarBrandsAsync();
        }

        public async Task<ServiceResponse> UpdateCarBrandAsync(CarBrand carBrand)
        {
            return await _carBrandRepository.UpdateCarBrandAsync(carBrand);
        }
    }
}
