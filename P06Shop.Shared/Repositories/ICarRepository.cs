using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using P06Shop.Shared.Cars;

namespace P06Shop.Shared.Repositories
{
    public interface ICarRepository
    {
        Task<ServiceResponse<List<Car>>> GetCarsAsync();

        Task<ServiceResponse> DeleteCarAsync(int carId);

        Task<ServiceResponse> UpdateCarAsync(Car car);

        Task<ServiceResponse> CreateCarAsync(Car car);
    }
}
