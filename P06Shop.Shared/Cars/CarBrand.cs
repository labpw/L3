using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P06Shop.Shared.Cars
{
    public class CarBrand
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OriginCountry { get; set; }

        public CarBrand() 
        { 
        }

        public CarBrand ( string name, string country) 
        { 
            Name = name;
            OriginCountry = country;
        }

        public override bool Equals (object obj)
        {
            if (obj == null || !(obj is CarBrand other))
            {
                return false;
            }

            return this.Id == other.Id
                && this.Name == other.Name
                && this.OriginCountry == other.OriginCountry;
        }
    }
}
