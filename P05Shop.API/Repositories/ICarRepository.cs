using P06Shop.Shared.Cars;

namespace P05Shop.API.Repositories
{
    public interface ICarRepository
    {
        void AddCar (Car car);
        void DeleteCar (int carId);
        List<Car> GetAllCars ();
        Car? GetCartById (int carId);
        void UpdateCar (Car updatedCar);
    }
}
