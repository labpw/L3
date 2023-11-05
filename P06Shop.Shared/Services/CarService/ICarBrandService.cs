using P06Shop.Shared.Cars;
using P06Shop.Shared;

public interface ICarBrandService
{
    Task<ServiceResponse<List<CarBrand>>> GetCarBrandsAsync();
    Task<ServiceResponse> DeleteCarBrandAsync(int carBrandId);
    Task<ServiceResponse> UpdateCarBrandAsync(CarBrand carBrand);
    Task<ServiceResponse> CreateCarBrandAsync(CarBrand carBrand);
}