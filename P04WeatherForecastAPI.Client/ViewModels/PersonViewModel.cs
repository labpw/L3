using P06Shop.API.Services.PersonService;
using P06Shop.Shared.Cars;
using P06Shop.Shared.Services.ProductService;
using P06Shop.Shared.Shop;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    public class PersonViewModel
    {
        private readonly IPersonService _personService;

        public ObservableCollection<Person> People { get; set; }

        public PersonViewModel(IPersonService personService)
        {
            _personService = personService;
            People = new ObservableCollection<Person>();
        }

        public async void GetPeople()
        {
            var peopleResult = await _personService.GetPeopleAsync();
            if (peopleResult.Success)
            {
                foreach (var p in peopleResult.Data)
                {
                    People.Add(p);
                }
            }
        }
    }
}
