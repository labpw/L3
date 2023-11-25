using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using P06Shop.Shared.Cars;

namespace P06Shop.Shared.Repositories
{
    public interface ICarBrandRepository
    {
        Task<ServiceResponse<List<CarBrand>>> GetCarBrandsAsync();
        Task<ServiceResponse> DeleteCarBrandAsync(int carBrandId);
        Task<ServiceResponse> UpdateCarBrandAsync(CarBrand carBrand);
        Task<ServiceResponse> CreateCarBrandAsync(CarBrand carBrand);
    }
}
