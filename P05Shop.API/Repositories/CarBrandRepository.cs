using Microsoft.EntityFrameworkCore;
using P05Shop.API.Repositories.Interfaces;
using P06Shop.Shared.Cars;

namespace P05Shop.API.Repositories
{
    public class CarBrandRepository : ICarBrandRepository
    {
        private DataBaseContext dataBaseContext;

        public CarBrandRepository (DataBaseContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
        }

        public async void AddCarBrand (CarBrand carBrand)
        {
            await dataBaseContext.CarBrands.AddAsync (carBrand);
            await dataBaseContext.SaveChangesAsync ();
        }

        public async void DeleteCarBrand (CarBrand carBrand)
        {
            dataBaseContext.CarBrands.Remove (carBrand);
            await dataBaseContext.SaveChangesAsync ();
        }

        public List<CarBrand> GetAllCarBrads ()
        {
            List<CarBrand> result = dataBaseContext.CarBrands.ToList ();
            return result;
        }

        public CarBrand? GetCarBrandById (int carBrandId)
        {
            CarBrand result = dataBaseContext.CarBrands.FirstOrDefault (cb => cb.Id == carBrandId);
            return result;
        }

        public async void UpdateCarBrand (CarBrand updatedCarBrand)
        {
            dataBaseContext.CarBrands.Update (updatedCarBrand);
            await dataBaseContext.SaveChangesAsync ();
        }
    }
}
