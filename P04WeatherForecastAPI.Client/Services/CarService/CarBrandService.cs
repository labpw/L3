using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using P04WeatherForecastAPI.Client.Configuration;
using P06Shop.Shared.Cars;
using P06Shop.Shared.Services.CarService;
using P06Shop.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Services.CarService
{
    internal class CarBrandService : ICarBrandService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;

        public CarBrandService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings.Value;
        }

        public async Task<ServiceResponse> CreateCarBrandAsync(CarBrand carBrand)
        {
            var json = JsonConvert.SerializeObject(carBrand);
            HttpContent contentToUpdate = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(string.Format(_appSettings.CarBrandsEndpoint.Create, carBrand.Id), contentToUpdate);
            var result = new ServiceResponse
            {
                Message = "Car Brands retrieved successfully.",
                Success = true
            };
            return result;
        }

        public async Task<ServiceResponse> DeleteCarBrandAsync(int carBrandId)
        {
            var response = await _httpClient.DeleteAsync(string.Format(_appSettings.CarBrandsEndpoint.Delete, carBrandId));
            var result = new ServiceResponse
            {
                Message = "Car Brands retrieved successfully.",
                Success = true
            };
            return result;
        }

        public async Task<ServiceResponse<List<CarBrand>>> GetCarBrandsAsync()
        {
            var response = await _httpClient.GetAsync(_appSettings.CarBrandsEndpoint.GetAll);
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
            var response = await _httpClient.PutAsync(string.Format(_appSettings.CarBrandsEndpoint.Update, carBrand.Id), contentToUpdate);
            var result = new ServiceResponse
            {
                Message = "Car Brands retrieved successfully.",
                Success = true
            };
            return result;
        }
    }

}
