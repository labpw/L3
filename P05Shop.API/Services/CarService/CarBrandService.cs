using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using P06Shop.Shared.Cars;
using P06Shop.Shared;
using P05Shop.API;
using Microsoft.EntityFrameworkCore;

namespace P06Shop.API.Services.CarBrandService
{
    public class CarBrandService : ICarBrandService
    {
        private DataBaseContext dataBaseContext;

        public CarBrandService(DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        public async Task<ServiceResponse<List<CarBrand>>> GetCarBrandsAsync()
        {
            var response = new ServiceResponse<List<CarBrand>>();
            try
            {
                response.Data = await dataBaseContext.CarBrands.ToListAsync();
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
                var carBrandToRemove = await dataBaseContext.CarBrands.FirstOrDefaultAsync(cb => cb.Id == carBrandId);
                if (carBrandToRemove != null)
                {
                    dataBaseContext.CarBrands.Remove(carBrandToRemove);
                    response.Success = true;
                    response.Message = "Car brand deleted successfully.";
                    await dataBaseContext.SaveChangesAsync();
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
                var existingCarBrand = await dataBaseContext.CarBrands.FirstOrDefaultAsync(cb => cb.Id == carBrand.Id);
                if (existingCarBrand != null)
                {
                    existingCarBrand.Name = carBrand.Name;
                    existingCarBrand.OriginCountry = carBrand.OriginCountry;
                    dataBaseContext.CarBrands.Update(existingCarBrand);
                    response.Success = true;
                    response.Message = "Car brand updated successfully.";
                    await dataBaseContext.SaveChangesAsync();
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
                await dataBaseContext.CarBrands.AddAsync(carBrand);
                response.Success = true;
                response.Message = "Car brand created successfully.";
                await dataBaseContext.SaveChangesAsync();
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
