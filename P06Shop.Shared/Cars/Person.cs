using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P06Shop.Shared.Cars
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }

        public Person ()
        {
            Id = 0;
            Name = string.Empty;
            PhoneNumber = string.Empty;
        }

        public Person (int id, string name, string phoneNumber)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
        }

        public override bool Equals (object obj)
        {
            if (obj == null || !(obj is Person other))
            {
                return false;
            }

            return this.Id == other.Id
                && this.Name == other.Name
                && this.PhoneNumber == other.PhoneNumber;
        }
    }
}
