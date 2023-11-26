using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P06Shop.Shared.Cars;
using P06Shop.Shared;
using P05Shop.API;
using Microsoft.EntityFrameworkCore;
using P05Shop.API.Repositories;
using P05Shop.API.Repositories.Interfaces;

namespace P06Shop.API.Services.CarBrandService
{
    public class CarBrandService : ICarBrandService
    {
        private DataBaseContext dataBaseContext;
        private readonly ICarBrandRepository _carBrandRepository;

        public CarBrandService(DataBaseContext dataBaseContext, ICarBrandRepository carBrandRepository)
        {
            this.dataBaseContext = dataBaseContext;
            this._carBrandRepository = carBrandRepository;
        }

        public async Task<ServiceResponse<List<CarBrand>>> GetCarBrandsAsync()
        {
            var response = new ServiceResponse<List<CarBrand>>();
            try
            {
                response.Data = _carBrandRepository.GetAllCarBrads();
                response.Success = true;
                response.Message = "Car brands retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error while retrieving car brands: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse> DeleteCarBrandAsync(int carBrandId)
        {
            var response = new ServiceResponse();
            try
            {
                var carBrandToRemove = _carBrandRepository.GetCarBrandById (carBrandId);
                if (carBrandToRemove != null)
                {
                    _carBrandRepository.DeleteCarBrand(carBrandToRemove);

                    response.Success = true;
                    response.Message = "Car brand deleted successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Car brand not found for deletion.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error while deleting car brand: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse> UpdateCarBrandAsync(CarBrand carBrand)
        {
            var response = new ServiceResponse();
            try
            {
                var existingCarBrand = _carBrandRepository.GetCarBrandById (carBrand.Id);
                if (existingCarBrand != null)
                {
                    existingCarBrand.Name = carBrand.Name;
                    existingCarBrand.OriginCountry = carBrand.OriginCountry;

                    _carBrandRepository.UpdateCarBrand (existingCarBrand);

                    response.Success = true;
                    response.Message = "Car brand updated successfully.";
                }
                else
                {
                    response.Success = false;
                    response.Message = "Car brand not found for updating.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error while updating car brand: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse> CreateCarBrandAsync(CarBrand carBrand)
        {
            var response = new ServiceResponse();
            try
            {
                _carBrandRepository.AddCarBrand (carBrand);
                response.Success = true;
                response.Message = "Car brand created successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error while creating car brand: " + ex.Message;
            }
            return response;
        }
    }
}
