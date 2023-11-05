using Bogus;
using P06Shop.Shared.Cars;
using P06Shop.Shared.Shop;
using Person = P06Shop.Shared.Cars.Person;

namespace P07Shop.DataSeeder
{
    public static class CarSeeder
    {
        public static List<Car> GenerateCarData()
        {
            int carId = 1;
            var carFaker = new Faker<Car>()
                .RuleFor(x => x.Model, x => x.Vehicle.Manufacturer())
                .RuleFor(x => x.Power, x => x.Random.Int(0, int.MaxValue))
                .RuleFor(x => x.Id, x => carId++)
                .RuleFor(x => x.CarBrandId, x => x.Random.Int(1, 25))
                .RuleFor(x => x.PreviousOwnerId, x => x.Random.Int(1, 25));

            return carFaker.Generate(25).ToList();

        }
    }
}
