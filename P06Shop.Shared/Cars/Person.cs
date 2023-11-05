using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P06Shop.Shared.Cars
{
    public class Person(int id, string name, string phoneNumber)
    {
        public int Id { get; set; } = id;
        public string Name { get; set; } = name;
        public string PhoneNumber { get; set; } = phoneNumber;

        public Person() : this(0, string.Empty, string.Empty) { }
    }
}
