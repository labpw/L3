using Bogus;
using P06Shop.Shared.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P07Shop.DataSeeder
{
    public class CarBrandSeeder
    {
        static CarBrandSeeder()
        { 
            Randomizer.Seed = new Random(2137);
        }

        public static List<CarBrand> GenerateCarBrandData()
        {
            int carBrandId = 1;
            var carBrandFaker = new Faker<CarBrand>()
                .RuleFor(x => x.Name, x => x.Vehicle.Manufacturer())
                .RuleFor(x => x.OriginCountry, x => x.Address.Country())
                .RuleFor(x => x.Id, x => carBrandId++);

            return carBrandFaker.Generate(25).ToList();

        }
    }
}
