using P06Shop.Shared.Cars;

namespace P05Shop.API.Repositories.Interfaces
{
    public interface ICarBrandRepository
    {
        void AddCarBrand(CarBrand carBrand);
        void DeleteCarBrand(CarBrand carBrand);
        List<CarBrand> GetAllCarBrads();
        CarBrand? GetCarBrandById(int carBrandId);
        void UpdateCarBrand(CarBrand updatedCarBrand);
    }
}
