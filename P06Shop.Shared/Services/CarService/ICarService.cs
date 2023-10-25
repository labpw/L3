using P06Shop.Shared.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P06Shop.Shared.Services.CarService
{
    public interface ICarService
    {
        Task<ServiceResponse<List<Car>>> GetCarsAsync();

        Task<ServiceResponse> DeleteCarAsync(int carId);

        Task<ServiceResponse> UpdateCarAsync(Car car);

        Task<ServiceResponse> CreateCarAsync(Car car);
    }
}
