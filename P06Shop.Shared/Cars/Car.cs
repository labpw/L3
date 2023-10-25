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

        [RegularExpression("^[a-zA-Z]*$")]
        public string Brand { get; set; }

        [Range(0, int.MaxValue)]
        public int Power { get; set; }

        public Car()
        {
            Id = 0;
            Brand = string.Empty;
            Power = 0;
        }

        public Car(string brand, int power)
        {
            Brand = brand;
            Power = power;
        }
    }
}
