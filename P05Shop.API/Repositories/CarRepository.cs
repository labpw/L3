using P05Shop.API.Repositories.Interfaces;
using P06Shop.Shared;
using P06Shop.Shared.Cars;

namespace P05Shop.API.Repositories
{
    public class CarRepository : ICarRepository
    {
        private DataBaseContext dataBaseContext;

        public CarRepository (DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        public async void AddCar (Car car)
        {
            await dataBaseContext.Cars.AddAsync (car);
            await dataBaseContext.SaveChangesAsync ();
        }

        public async void DeleteCar (Car car)
        {
            dataBaseContext.Cars.Remove (car);
            await dataBaseContext.SaveChangesAsync ();
        }

        public List<Car> GetAllCars ()
        {
            List<Car> result = dataBaseContext.Cars.ToList ();
            return result;
        }

        public Car? GetCarById (int carId)
        {
            Car result = dataBaseContext.Cars.FirstOrDefault (cb => cb.Id == carId);
            return result;
        }

        public async void UpdateCar (Car updatedCar)
        {
            dataBaseContext.Cars.Update (updatedCar);
            await dataBaseContext.SaveChangesAsync ();
        }
    }
}
