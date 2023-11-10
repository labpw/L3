using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using P04WeatherForecastAPI.Client.Configuration;
using P06Shop.Shared;
using P06Shop.Shared.Cars;
using P06Shop.Shared.Services.CarService;
using System.Net.Http;
using System.Text;

namespace MVCClient.Services
{
    public class CarBrandService : ICarBrandService
    {
        private readonly AppSettings appSettings;
        private readonly HttpClient httpClient;

        public CarBrandService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
            this.httpClient = httpClient;
        }

        public async Task<ServiceResponse> CreateCarBrandAsync(CarBrand carBrand)
        {
            var json = JsonConvert.SerializeObject(carBrand);
            HttpContent contentToUpdate = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(string.Format(appSettings.CarBrandsEndpoint.Create, carBrand.Id), contentToUpdate);
            var result = new ServiceResponse
            {
                Message = "Car Brands retrieved successfully.",
                Success = true
            };
            return result;
        }

        public async Task<ServiceResponse> DeleteCarBrandAsync(int carBrandId)
        {
            var response = await httpClient.DeleteAsync(string.Format(appSettings.CarBrandsEndpoint.Delete, carBrandId));
            var result = new ServiceResponse
            {
                Message = "Car Brands retrieved successfully.",
                Success = true
            };
            return result;
        }

        public async Task<ServiceResponse<List<CarBrand>>> GetCarBrandsAsync()
        {
            var response = await httpClient.GetAsync(appSettings.CarBrandsEndpoint.GetAll);
            var json = await response.Content.ReadAsStringAsync();
            var content = JsonConvert.DeserializeObject<List<CarBrand>>(json);
            var result = new ServiceResponse<List<CarBrand>>
            {
                Data = content,
                Message = "Car Brands retrieved successfully.",
                Success = true
            };
            return result;
        }

        public async Task<ServiceResponse> UpdateCarBrandAsync(CarBrand carBrand)
        {
            var json = JsonConvert.SerializeObject(carBrand);
            HttpContent contentToUpdate = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(string.Format(appSettings.CarBrandsEndpoint.Update, carBrand.Id), contentToUpdate);
            var result = new ServiceResponse
            {
                Message = "Car Brands retrieved successfully.",
                Success = true
            };
            return result;
        }
    }
}
