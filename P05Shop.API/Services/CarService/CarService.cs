using P06Shop.Shared.Cars;
using P06Shop.Shared;
using P06Shop.Shared.Services.CarService;
using P07Shop.DataSeeder;
using P06Shop.API.Services.PersonService;
using Microsoft.EntityFrameworkCore;

namespace P05Shop.API.Services.CarService
{
    public class CarService : ICarService
    {
        private DataBaseContext dataBaseContext;
        private readonly IPersonService personService;
        private readonly ICarBrandService carBrandService;

        public CarService(DataBaseContext dataBaseContext, IPersonService personService, ICarBrandService carBrandService)
        {
            this.dataBaseContext = dataBaseContext;
            this.carBrandService = carBrandService;
            this.personService = personService;
        }

        public async Task<ServiceResponse<List<Car>>> GetCarsAsync()
        {
            var response = new ServiceResponse<List<Car>>();
            try
            {
                response.Data = dataBaseContext.Cars.Include("CarBrand").Include("PreviousOwner").ToList();
                response.Success = true;
                response.Message = "Cars retrieved successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error while retrieving cars: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse> DeleteCarAsync(int carId)
        {
            var response = new ServiceResponse();
            try
            {
                var carToRemove = dataBaseContext.Cars.FirstOrDefault(c => c.Id == carId);
                if (carToRemove != null)
                {
                    dataBaseContext.Cars.Remove(carToRemove);
                    response.Success = true;
                    response.Message = "Car deleted successfully.";
                    await dataBaseContext.SaveChangesAsync();
                }
                else
                {
                    response.Success = false;
                    response.Message = "Car not found for deletion.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error while deleting car: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse> UpdateCarAsync(Car car)
        {
            var response = new ServiceResponse();
            try
            {
                await ValidateCar(car);
                var existingCar = dataBaseContext.Cars.FirstOrDefault(c => c.Id == car.Id);
                if (existingCar != null)
                {
                    existingCar.Model = car.Model;
                    existingCar.Power = car.Power;
                    existingCar.CarBrand = car.CarBrand;
                    existingCar.PreviousOwner = car.PreviousOwner;
                    response.Success = true;
                    response.Message = "Car updated successfully.";
                    await dataBaseContext.SaveChangesAsync();
                }
                else
                {
                    response.Success = false;
                    response.Message = "Car not found for updating.";
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error while updating car: " + ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse> CreateCarAsync(Car car)
        {
            var response = new ServiceResponse();
            try
            {
                await ValidateCar(car);
                dataBaseContext.Cars.Add(car);
                response.Success = true;
                response.Message = "Car created successfully.";
                await dataBaseContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error while creating car: " + ex.Message;
            }
            return response;
        }
        private async Task ValidateCar(Car car)
        {
            if (!await DoesPreviousOwnerOfCarExist(car))
            {
                throw new Exception("Previous owner does not exist.");
            }
            if(!await DoesBrandOfCarExist(car))
            {
                throw new Exception("Car Brand does not exist.");
            }
        }

        private async Task<bool> DoesPreviousOwnerOfCarExist(Car car)
        {
            return car.PreviousOwnerId == null || !(await personService.GetPeopleAsync()).Data.Any(e => e.Id == car.PreviousOwnerId);
        }

        private async Task<bool> DoesBrandOfCarExist(Car car)
        {
            return (await carBrandService.GetCarBrandsAsync()).Data.Any(e => e.Id == car.CarBrandId);
        }
    }
}
