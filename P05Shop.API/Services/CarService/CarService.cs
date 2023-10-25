using P06Shop.Shared.Cars;
using P06Shop.Shared;
using P06Shop.Shared.Services.CarService;
using P07Shop.DataSeeder;

namespace P05Shop.API.Services.CarService
{
    public class CarService : ICarService
    {
        private static List<Car> cars = CarSeeder.GenerateCarData();

        public async Task<ServiceResponse<List<Car>>> GetCarsAsync()
        {
            var response = new ServiceResponse<List<Car>>();
            try
            {
                response.Data = cars;
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
                var carToRemove = cars.FirstOrDefault(c => c.Id == carId);
                if (carToRemove != null)
                {
                    cars.Remove(carToRemove);
                    response.Success = true;
                    response.Message = "Car deleted successfully.";
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
                var existingCar = cars.FirstOrDefault(c => c.Id == car.Id);
                if (existingCar != null)
                {
                    existingCar.Brand = car.Brand;
                    existingCar.Power = car.Power;
                    response.Success = true;
                    response.Message = "Car updated successfully.";
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
                // Generate a new unique Id for the car and add it to the list.
                car.Id = Random.Shared.Next();
                cars.Add(car);
                response.Success = true;
                response.Message = "Car created successfully.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error while creating car: " + ex.Message;
            }
            return response;
        }
    }
}
