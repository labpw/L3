using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P06Shop.Shared.Cars
{
    public class Car
    {
        public int Id { get; set; }

        public string Model { get; set; }

        [Range(0, int.MaxValue)]
        public int Power { get; set; }

        public int CarBrandId { get; set; }
        public CarBrand? CarBrand { get; set; }

        public int? PreviousOwnerId { get; set; }
        public Person? PreviousOwner { get; set; }

        public Car()
        {
            Id = 0;
            Model = string.Empty;
            Power = 0;
        }

        public Car(string model, int power)
        {
            Model = model;
            Power = power;
        }

        public Car (CarBrand brand, Person owenr, string model, int power)
        {
            CarBrand = brand;
            PreviousOwner = owenr;
            Model = model;
            Power = power;
        }
    }
}
