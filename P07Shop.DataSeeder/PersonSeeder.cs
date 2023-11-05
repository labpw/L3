using Bogus;
using P06Shop.Shared.Cars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Person = P06Shop.Shared.Cars.Person;

namespace P07Shop.DataSeeder
{
    public class PersonSeeder
    {
        public static List<Person> GeneratePersonData()
        {
            int personId = 1;
            var personFaker = new Faker<Person>()
                .RuleFor(x => x.Name, x => x.Person.FullName)
                .RuleFor(x => x.PhoneNumber, x => x.Person.Phone)
                .RuleFor(x => x.Id, x => personId++);

            return personFaker.Generate(25).ToList();

        }
    }
}
