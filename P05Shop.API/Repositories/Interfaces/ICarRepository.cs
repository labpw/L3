using P06Shop.Shared.Cars;

namespace P05Shop.API.Repositories.Interfaces
{
    public interface ICarRepository
    {
        void AddCar(Car car);
        void DeleteCar(Car car);
        List<Car> GetAllCars();
        Car? GetCarById(int carId);
        void UpdateCar(Car updatedCar);
    }
}
